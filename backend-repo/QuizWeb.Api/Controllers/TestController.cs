using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizWeb.Application.DTOs.Quizzes;
using QuizWeb.Application.DTOs.Tests;
using QuizWeb.Application.Interfaces.Tests;
using System.Security.Claims;

namespace QuizWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;
        private readonly IPracticeService _practiceService;

        public TestController(ITestService testService, IPracticeService practiceService)
        {
            _testService = testService;
            _practiceService = practiceService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTest([FromBody] CreateTestRequest request)
        {
            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = await _testService.CreateTest(userId, request);
                return StatusCode(result.StatusCode, result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTestsByUser([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = await _testService.GetTestListByUserId(userId, page, pageSize);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{testId}/questions")]
        public async Task<IActionResult> GetTestQuestions(int testId, [FromQuery] int userId)
        {
            var questions = await _testService.GenerateQuestionsForTestAsync(testId, userId);
            return Ok(questions);
        }
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitAnswers([FromBody] SubmitAnswerDto dto)
        {
            await _testService.SubmitAnswersAsync(dto);
            return Ok(new { message = "Submitted successfully" });
        }
        [HttpGet("practice/{topicId}")]
        public async Task<IActionResult> GetPracticeQuestion(int topicId)
        {
            var question = await _practiceService.GetRandomPracticeQuestionAsync(topicId);
            if (question == null)
                return BadRequest("Topic must contain at least 4 questions.");

            return Ok(question);
        }
        [HttpPost("practice/check")]
        public async Task<IActionResult> CheckPracticeAnswer([FromBody] PracticeAnswerCheckRequest request)
        {
            var result = await _practiceService.CheckPracticeAnswerAsync(request);
            if (result == null)
                return NotFound("Quiz not found");

            return Ok(result);
        }

    }
}
