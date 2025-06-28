using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWeb.Application.DTOs.Tests
{
    public class SubmitAnswerDto
    {
        public int TestId { get; set; }
        public int UserId { get; set; }
        public List<SubmittedQuestionAnswer> Answers { get; set; } = new();
    }

    public class SubmittedQuestionAnswer
    {
        public int QuizId { get; set; }
        public string SelectedAnswer { get; set; }
    }

}
