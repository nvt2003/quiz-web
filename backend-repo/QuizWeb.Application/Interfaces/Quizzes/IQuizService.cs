using QuizWeb.Application.DTOs.Quizzes;
using QuizWeb.Application.DTOs.Response;
using QuizWeb.Application.DTOs.Topics;

namespace QuizWeb.Application.Interfaces.Quizzes
{
    public interface IQuizService
    {
        Task<PagedResponse<QuizVM>> GetQuizByTopicId(int topicId, int page, int pageSize);
        Task<ApiResponse<string>> CreateQuiz(List<CreateQuizDto> quizzes);
        Task<ApiResponse<List<QuizVM>>> GetQuizzes(int topicId);
        Task<ApiResponse<string>> UpdateQuiz(List<CreateQuizDto> quizzes);
    }
}
