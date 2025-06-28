using QuizWeb.Application.DTOs.Quizzes;
namespace QuizWeb.Application.Interfaces.Tests
{
    public interface IPracticeService
    {
        Task<PracticeQuestionDto?> GetRandomPracticeQuestionAsync(int topicId);
        Task<PracticeAnswerCheckResult?> CheckPracticeAnswerAsync(PracticeAnswerCheckRequest request);
    }
}
