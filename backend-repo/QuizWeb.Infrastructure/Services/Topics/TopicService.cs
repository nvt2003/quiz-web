using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using QuizWeb.Application.DTOs.Response;
using QuizWeb.Application.DTOs.Topics;
using QuizWeb.Application.Interfaces.Topics;
using QuizWeb.Infrastructure.Persistence;
using QuizWeb.Infrastructure.Persistence.Models;
using System.Globalization;
using System.Linq;
using System.Text;

namespace QuizWeb.Infrastructure.Services.Topics
{
    public class TopicService : ITopicService
    {
        private readonly QuizDbContext _context;
        private readonly IMapper _mapper;

        public TopicService(QuizDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TopicDto> CreateTopicAsync(CreateTopicRequest request)
        {
            var topic = _mapper.Map<Topic>(request);
            _context.Topics.Add(topic);
            await _context.SaveChangesAsync();

            return _mapper.Map<TopicDto>(topic);
        }

        public Task<ApiResponse<bool>> DeleteTopic(int topicId)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResponse<TopicDto>> GetTopic(int page, int pageSize)
        {
            try
            {
                var count = _context.Topics.Count();
                var topic = await _context.Topics
                    .Include(t => t.User)
                    .Include(t => t.Category)
                    .Skip((page - 1) * pageSize).Take(pageSize)
                    .ToListAsync();
                var topics = _mapper.Map<List<TopicDto>>(topic);
                return new PagedResponse<TopicDto>(topics, page, pageSize, count, "Fetch topics successfully!", 200);
            }
            catch (Exception ex)
            {
                return new PagedResponse<TopicDto>(null, page, pageSize, 0, "Error while fetch topics!", 200);
            }
        }

        public async Task<ApiResponse<TopicVM>> GetTopicById(int topicId)
        {
            try
            {
                var topic = await _context.Topics
                    .Include(t => t.User)
                    .Include(t => t.Quizzes)
                    .Include(t => t.Category)
                    .FirstOrDefaultAsync(t => t.Id == topicId);
                if (topic == null)
                    return new ApiResponse<TopicVM>(404, "Cannot found any topic with id " + topicId, null);
                var topicVm = _mapper.Map<TopicVM>(topic);
                return new ApiResponse<TopicVM>(200, "", topicVm);
            }catch (Exception ex)
            {
                return new ApiResponse<TopicVM>(400,"Error while get from db",null);
            }
        }

        public async Task<PagedResponse<TopicDto>> GetTopicByUserId(int userId,int page,int pageSize)
        {
            try
            {
                var count = _context.Topics
                    .Where(t => t.UserId == userId).Count();
                var topic = _context.Topics
                    .Include(t => t.User)
                    .Include(t => t.Category)
                    .Where(t => t.UserId==userId)
                    .Skip((page-1)*pageSize).Take(pageSize)
                    .ToList();
                var topics = _mapper.Map<List<TopicDto>>(topic);
                return new PagedResponse<TopicDto>(topics,page,pageSize,count,"Fetch topics by user id successfully!",200);
            }
            catch (Exception ex)
            {
                return new PagedResponse<TopicDto>(null, page, pageSize, 0, "Error while fetch topics!", 200);
            }
        }

        public async Task<bool> IsTopicTitleDuplicateAsync(int userId, string title)
        {
            return await _context.Topics.AnyAsync(t =>
                t.UserId == userId &&
                t.Title.ToLower() == title.ToLower());
        }

        public Task<TopicDto> UpdateTopicAsync(UpdateTopicRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResponse<TopicDto>> SearchTopic(int page, int pageSize, 
            int? userId, int? categoryId, string? title, DateTime? createdAtStart, DateTime? createdAtEnd)
        {
            try
            {
                var count = _context.Topics
                    .Include(t => t.Category)
                    .Include(t => t.User)
                    .Where(t =>
                        (userId.HasValue ? t.UserId == userId : true) &&
                        (categoryId.HasValue ? t.CategoryId == categoryId : true) &&
                        (createdAtStart.HasValue ? t.CreatedAt >= createdAtStart : true) &&
                        (createdAtEnd.HasValue ? t.CreatedAt <= createdAtEnd : true)
                    )
                    .AsEnumerable()
                    .Where(t =>
                        title != null ? RemoveVietnameseTone(t.Title.ToLower()).Contains(RemoveVietnameseTone(title.ToLower())) : true
                    ).Count();
                var topics = _context.Topics
                    .Include(t => t.Category)
                    .Include(t => t.User)
                    .Where(t =>
                        (userId.HasValue ? t.UserId == userId : true) &&
                        (categoryId.HasValue ? t.CategoryId == categoryId : true) &&
                        (createdAtStart.HasValue ? t.CreatedAt >= createdAtStart : true) &&
                        (createdAtEnd.HasValue ? t.CreatedAt <= createdAtEnd : true)
                    )
                    .AsEnumerable()
                    .Where(t =>
                        title != null ? RemoveVietnameseTone(t.Title.ToLower()).Contains(RemoveVietnameseTone(title.ToLower())) : true
                    )
                    .ToList();


                var topicDtos = _mapper.Map<List<TopicDto>>(topics);

                return new PagedResponse<TopicDto>(topicDtos, page, pageSize, count, "Search topics successfully!", 200);
            }
            catch (Exception ex)
            {
                return new PagedResponse<TopicDto>(null, page, pageSize, 0, $"Error while fetching topics: {ex.Message}", 500);
            }
        }
        public static string RemoveVietnameseTone(string input)
        {
            var normalizedString = input.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var chr in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(chr);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(chr);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }

}
