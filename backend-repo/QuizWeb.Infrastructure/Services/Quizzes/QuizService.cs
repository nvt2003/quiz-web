using Azure;
using Microsoft.EntityFrameworkCore;
using QuizWeb.Application.DTOs.Quizzes;
using QuizWeb.Application.DTOs.Response;
using QuizWeb.Application.Interfaces.Quizzes;
using QuizWeb.Infrastructure.Persistence;
using QuizWeb.Infrastructure.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace QuizWeb.Infrastructure.Services.Quizzes
{
    public class QuizService : IQuizService
    {
        private readonly QuizDbContext _context;

        public QuizService(QuizDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<string>> CreateQuiz(List<CreateQuizDto> quizzes)
        {
            var entities = quizzes.Select(q => new Quiz
            {
                TopicId = q.TopicId,
                Question = q.Question,
                Answer = q.Answer
            });

            await _context.Quizzes.AddRangeAsync(entities);
            await _context.SaveChangesAsync();

            return new ApiResponse<string>(200, "Quizzes created successfully", null);
        }

        public async Task<PagedResponse<QuizVM>> GetQuizByTopicId(int topicId, int page, int pageSize)
        {
            var query = _context.Quizzes
                .Where(q => q.TopicId == topicId)
                .OrderBy(q => q.Id);

            var totalItems = query.Count();

            var items = await query.Skip((page - 1) * pageSize).Take(pageSize)
                .Select(q => new QuizVM
                {
                    Id = q.Id,
                    TopicId = q.TopicId,
                    Question = q.Question,
                    Answer = q.Answer
                })
                .ToListAsync();

            return PagedResponse<QuizVM>.Create(items, page, pageSize, totalItems, "Success", 200);
        }
        public async Task<ApiResponse<List<QuizVM>>> GetQuizzes(int topicId)
        {
            var items = await _context.Quizzes
                .Where(q => q.TopicId == topicId)
                .OrderBy(q => q.Id)
                .Select(q => new QuizVM
                {
                    Id = q.Id,
                    TopicId = q.TopicId,
                    Question = q.Question,
                    Answer = q.Answer
                })
            .ToListAsync();

            return new ApiResponse<List<QuizVM>>(200, "Success", items);
        }

        public async Task<ApiResponse<string>> UpdateQuiz(List<CreateQuizDto> quizzes)
        {
            if (quizzes == null || quizzes.Count == 0)
            {
                return new ApiResponse<string>(400, "No quizzes provided for update.", null);
            }
            // every quizzes in list have same topicid
            var topicId = quizzes.First().TopicId; 

            var existingQuizzes = await _context.Quizzes
                .Where(q => q.TopicId == topicId)
                .ToListAsync();
            //delete old quizzes
            if (existingQuizzes.Count > 0)
            {
                _context.Quizzes.RemoveRange(existingQuizzes);
            }

            var newQuizzes = quizzes.Select(q => new Quiz
            {
                TopicId = q.TopicId,
                Question = q.Question,
                Answer = q.Answer
            }).ToList();

            _context.Quizzes.AddRange(newQuizzes);

            await _context.SaveChangesAsync();

            return new ApiResponse<string>(200, "Quizzes updated successfully", null);
        }
    }
}
