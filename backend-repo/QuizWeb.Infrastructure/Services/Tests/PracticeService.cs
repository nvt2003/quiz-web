using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizWeb.Application.DTOs.Quizzes;
using QuizWeb.Application.DTOs.Tests;
using QuizWeb.Application.Interfaces.Tests;
using QuizWeb.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWeb.Infrastructure.Services.Tests
{
    public class PracticeService :IPracticeService
    {
        private readonly QuizDbContext _context;
        private readonly IMapper _mapper;

        public PracticeService(QuizDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PracticeQuestionDto?> GetRandomPracticeQuestionAsync(int topicId)
        {
            var allQuizzes = await _context.Quizzes
                .Where(q => q.TopicId == topicId)
                .ToListAsync();
            // require 4 question
            if (allQuizzes.Count < 4)
                return null;

            var random = new Random();
            var selectedQuiz = allQuizzes[random.Next(allQuizzes.Count)];

            var wrongAnswers = allQuizzes
                .Where(q => q.Id != selectedQuiz.Id)
                .OrderBy(x => random.Next())
                .Take(3)
                .Select(q => q.Answer)
                .ToList();

            var options = wrongAnswers.Append(selectedQuiz.Answer)
                .OrderBy(x => random.Next())
                .ToList();

            return new PracticeQuestionDto
            {
                QuizId = selectedQuiz.Id,
                Question = selectedQuiz.Question,
                Options = options
            };
        }
        public async Task<PracticeAnswerCheckResult?> CheckPracticeAnswerAsync(PracticeAnswerCheckRequest request)
        {
            var quiz = await _context.Quizzes.FindAsync(request.QuizId);
            if (quiz == null)
                return null;

            bool isCorrect = string.Equals(
                quiz.Answer.Trim(),
                request.SelectedAnswer.Trim(),
                StringComparison.OrdinalIgnoreCase
            );

            return new PracticeAnswerCheckResult
            {
                IsCorrect = isCorrect,
                CorrectAnswer = quiz.Answer
            };
        }

    }
}
