using QuizWeb.Application.DTOs.Participants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWeb.Application.DTOs.Tests
{
    public class TestVM
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string DisplayName { get; set; }

        public int TopicId { get; set; }
        public string Title { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
        public virtual ICollection<ParticipantVM>? Participants { get; set; } = new List<ParticipantVM>();
    }
}
