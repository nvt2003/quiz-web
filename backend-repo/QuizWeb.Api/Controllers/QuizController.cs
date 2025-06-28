using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizWeb.Application.DTOs.Quizzes;
using QuizWeb.Application.Interfaces.Quizzes;

namespace QuizWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }
        [HttpGet("by-topic/{topicId}")]
        public async Task<IActionResult> GetQuizzesByTopic(int topicId, int page = 1, int pageSize = 10)
        {
            var result = await _quizService.GetQuizByTopicId(topicId, page, pageSize);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("to-update/{topicId}")]
        public async Task<IActionResult> GetQuizzes(int topicId)
        {
            var result = await _quizService.GetQuizzes(topicId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuizzes([FromBody] List<CreateQuizDto> quizzes)
        {
            var result = await _quizService.CreateQuiz(quizzes);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuizzes([FromBody] List<CreateQuizDto> quizzes)
        {
            var result = await _quizService.UpdateQuiz(quizzes);
            return StatusCode(result.StatusCode, result);
        }

    }
}
