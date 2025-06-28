using QuizWeb.Application.DTOs.Quizzes;
using QuizWeb.Application.DTOs.Response;
using QuizWeb.Application.DTOs.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWeb.Application.Interfaces.Tests
{
    public interface ITestService
    {
        Task<PagedResponse<TestVM>> GetTestListByUserId(int userId, int page, int pageSize);
        Task<ApiResponse<CreateTestRequest>> CreateTest(int userId, CreateTestRequest request);
        Task<ApiResponse<TestVM>> GetTestById(int id);
        Task<ApiResponse<JoinTestRequest>> JoinTestAsync(JoinTestRequest request);
        Task<List<QuizQuestionDto>> GenerateQuestionsForTestAsync(int testId, int userId);
        Task SubmitAnswersAsync(SubmitAnswerDto dto);
    }
}
