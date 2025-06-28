
using static System.Net.Mime.MediaTypeNames;

namespace QuizWeb.Application.DTOs.Participants
{
    public class ParticipantVM
    {
        public int Id { get; set; }

        public int TestId { get; set; }

        public int UserId { get; set; }
        public double? Score { get; set; }
    }
}
