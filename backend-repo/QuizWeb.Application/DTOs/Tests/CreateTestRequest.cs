using QuizWeb.Application.DTOs.Participants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWeb.Application.DTOs.Tests
{
    public class CreateTestRequest
    {
        public int TopicId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
        public virtual ICollection<ParticipantVM>? Participants { get; set; } = new List<ParticipantVM>();
    }
}
