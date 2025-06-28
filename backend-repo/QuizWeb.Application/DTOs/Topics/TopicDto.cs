using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWeb.Application.DTOs.Topics
{
    public class TopicDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }
    }

}
