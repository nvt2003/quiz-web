using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWeb.Application.DTOs.Tests
{
    public class JoinTestRequest
    {
        public int TestId { get; set; }
        public int? UserId { get; set; }
        public string? DisplayName { get; set; }
    }
}
