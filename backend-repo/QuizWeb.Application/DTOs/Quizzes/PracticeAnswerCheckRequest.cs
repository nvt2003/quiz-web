
namespace QuizWeb.Application.DTOs.Quizzes
{
    public class PracticeAnswerCheckRequest
    {
        public int QuizId { get; set; }
        public string SelectedAnswer { get; set; } = string.Empty;
    }

}
