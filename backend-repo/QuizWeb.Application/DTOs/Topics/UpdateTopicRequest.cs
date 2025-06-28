using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWeb.Application.DTOs.Topics
{
    public class UpdateTopicRequest
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int CategoryId { get; set; }

        public string Title { get; set; } = null!;
        public string? Description { get; set; }
    }

}
