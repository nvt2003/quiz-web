using QuizWeb.Application.DTOs.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace QuizWeb.Application.DTOs.Topics
{
    public class TopicVM
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string DisplayName { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<QuizVM> Quizzes { get; set; } = new List<QuizVM>();
    }
}
