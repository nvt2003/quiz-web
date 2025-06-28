using QuizWeb.Application.DTOs.Topics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWeb.Application.DTOs.Quizzes
{
    public class QuizVM
    {
        public int Id { get; set; }

        public int TopicId { get; set; }

        public string Question { get; set; } = null!;

        public string Answer { get; set; } = null!;
    }
}
