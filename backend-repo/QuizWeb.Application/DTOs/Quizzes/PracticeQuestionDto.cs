using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWeb.Application.DTOs.Quizzes
{
    public class PracticeQuestionDto
    {
        public int QuizId { get; set; }
        public string Question { get; set; }
        public List<string> Options { get; set; } = new();
    }
}
