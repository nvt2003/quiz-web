using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizWeb.Application.DTOs.Quizzes;
using QuizWeb.Application.DTOs.Response;
using QuizWeb.Application.DTOs.Tests;
using QuizWeb.Application.Interfaces.Tests;
using QuizWeb.Infrastructure.Persistence;
using QuizWeb.Infrastructure.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace QuizWeb.Infrastructure.Services.Tests
{
    public class TestService : ITestService
    {
        private readonly QuizDbContext _context;
        private readonly IMapper _mapper;

        public TestService(QuizDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CreateTestRequest>> CreateTest(int userId, CreateTestRequest request)
        {
            try
            {
                var topic = await _context.Topics.FindAsync(request.TopicId);
                if (topic == null)
                    return new ApiResponse<CreateTestRequest>(404, "Topic not found!", null);

                var participantIds = request.Participants?
                    .Select(p => p.UserId)
                    .Distinct()
                    .ToList();

                var participantIdString = participantIds != null && participantIds.Any()
                    ? string.Join(",", participantIds)
                    : "";

                var sql = "EXEC CreateTestAndAddParticipants @UserId = {0}, @TopicId = {1}, @StartTime = {2}, @EndTime = {3}, @ParticipantIds = {4}";

                await _context.Database.ExecuteSqlRawAsync(sql,
                    userId,
                    request.TopicId,
                    request.StartTime,
                    request.EndTime,
                    participantIdString
                );

                return new ApiResponse<CreateTestRequest>(200, "Test created successfully", request);
            }catch (Exception ex)
            {
                return new ApiResponse<CreateTestRequest>(400, "Error while created test", null);
            }
        }

        public async Task<PagedResponse<TestVM>> GetTestListByUserId(int userId, int page, int pageSize)
        {
            var query = _context.Tests
                .Where(t => t.UserId == userId)
                .Join(_context.Users,
                      t => t.UserId,
                      u => u.Id,
                      (t, u) => new { t, u })
                .Join(_context.Topics,
                      tu => tu.t.TopicId,
                      tp => tp.Id,
                      (tu, tp) => new TestVM
                      {
                          Id = tu.t.Id,
                          UserId = tu.u.Id,
                          DisplayName = tu.u.DisplayName,
                          TopicId = tp.Id,
                          Title = tp.Title,
                          StartTime = tu.t.StartTime,
                          EndTime = tu.t.EndTime
                      });

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(t => t.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return PagedResponse<TestVM>.Create(items, page, pageSize, totalCount, "Fetched tests successfully", 200);
        }
        public async Task<ApiResponse<TestVM>> GetTestById(int id)
        {
            var test = await _context.Tests
                .Include(t => t.User)
                .Include(t => t.Topic)
                .Include(t => t.Participants)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (test == null)
                return new ApiResponse<TestVM>(404, "Test not found", null);

            var testVm = _mapper.Map<TestVM>(test);

            return new ApiResponse<TestVM>(200, "Test retrieved successfully", testVm);
        }
        public async Task<ApiResponse<JoinTestRequest>> JoinTestAsync(JoinTestRequest request)
        {
            var test = await _context.Tests.FindAsync(request.TestId);
            if (test == null)
                return new ApiResponse<JoinTestRequest>(404,"Test not found",null);

            int userId;

            if (request.UserId.HasValue && request.UserId.Value > 0)
            {
                //if user had account
                var existingUser = await _context.Users.FindAsync(request.UserId.Value);
                if (existingUser == null)
                    return new ApiResponse<JoinTestRequest>(404, "User does not exist", null);

                userId = existingUser.Id;
            }
            else
            {
                //if join as guest, must have name
                if (string.IsNullOrWhiteSpace(request.DisplayName))
                    return new ApiResponse<JoinTestRequest>(400, "Guest display name is required", null);

                var guest = new User
                {
                    DisplayName = request.DisplayName,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Users.Add(guest);
                await _context.SaveChangesAsync();
                userId = guest.Id;
            }

            // check user is join or not
            bool alreadyJoined = await _context.Participants
                .AnyAsync(p => p.TestId == request.TestId && p.UserId == userId);

            if (!alreadyJoined)
            {
                var participant = new Participant
                {
                    TestId = request.TestId,
                    UserId = userId
                };

                _context.Participants.Add(participant);
                await _context.SaveChangesAsync();
            }
            var response = new JoinTestRequest
            {
                TestId = request.TestId,
                UserId = userId,
                DisplayName = request.DisplayName
            };
            return new ApiResponse<JoinTestRequest>(400, "Guest display name is required",response);
        }
        public async Task<List<QuizQuestionDto>> GenerateQuestionsForTestAsync(int testId, int userId)
        {
            var test = await _context.Tests
                .Include(t => t.Topic)
                .FirstOrDefaultAsync(t => t.Id == testId);

            if (test == null)
                throw new Exception("Test not found");

            var allQuizzes = await _context.Quizzes
                .Where(q => q.TopicId == test.TopicId)
                .ToListAsync();

            var random = new Random();
            var result = new List<QuizQuestionDto>();

            foreach (var quiz in allQuizzes)
            {
                var wrongAnswers = allQuizzes
                    .Where(q => q.Id != quiz.Id)
                    .OrderBy(_ => random.Next())
                    .Take(3)
                    .Select(q => q.Answer)
                    .ToList();

                var options = wrongAnswers.Append(quiz.Answer).OrderBy(_ => random.Next()).ToList();

                result.Add(new QuizQuestionDto
                {
                    QuizId = quiz.Id,
                    Question = quiz.Question,
                    Options = options
                });
            }

            return result;
        }
        public async Task SubmitAnswersAsync(SubmitAnswerDto dto)
        {
            var quizDict = await _context.Quizzes
                .Where(q => dto.Answers.Select(a => a.QuizId).Contains(q.Id))
                .ToDictionaryAsync(q => q.Id, q => q.Answer);

            foreach (var answer in dto.Answers)
            {
                bool isCorrect = quizDict[answer.QuizId].Trim() == answer.SelectedAnswer.Trim();

                var result = await _context.Results.FirstOrDefaultAsync(r =>
                    r.TestId == dto.TestId &&
                    r.UserId == dto.UserId &&
                    r.QuizId == answer.QuizId);

                if (result != null)
                {
                    result.IsTrue = isCorrect;
                    result.AttemptTime = DateTime.UtcNow;
                }
            }

            await _context.SaveChangesAsync();

            // Gọi proc tính điểm
            await _context.Database.ExecuteSqlRawAsync("EXEC CalculateTestScore @p0", dto.TestId);
        }

    }
}