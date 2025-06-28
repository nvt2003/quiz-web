using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizWeb.Application.DTOs.Response;
using QuizWeb.Application.DTOs.Topics;
using QuizWeb.Application.Interfaces.Topics;
using QuizWeb.Infrastructure.Persistence.Models;
using System.Security.Claims;

namespace QuizWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService _topicService;

        public TopicsController(ITopicService topicService)
        {
            _topicService = topicService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTopic([FromBody] CreateTopicRequest request)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var isDuplicate = await _topicService.IsTopicTitleDuplicateAsync(userId, request.Title);
            if (isDuplicate)
                return BadRequest(new ApiResponse<string>(400, "Topic title already exists for this user.", null));

            request.UserId = userId;

            var created = await _topicService.CreateTopicAsync(request);
            return Ok(new ApiResponse<TopicDto>(200, "Topic created successfully", created));
        }
        [Authorize]
        [HttpGet("MyTopic")]
        public async Task<IActionResult> GetTopicByUserId(int page, int pageSize)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var topics = await _topicService.GetTopicByUserId(userId, page, pageSize);
            return StatusCode(topics.StatusCode, topics);
        }

        [HttpGet]
        public async Task<IActionResult> GetTopic(int page, int pageSize)
        {
            var topics = await _topicService.GetTopic(page, pageSize);
            return StatusCode(topics.StatusCode, topics);
        }
        [HttpGet("{topicId}")]
        public async Task<IActionResult> GetTopicById(int topicId)
        {
            var topics = await _topicService.GetTopicById(topicId);
            return StatusCode(topics.StatusCode, topics);
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchTopics(int page, int pageSize, 
            int? userId, int? categoryId, string? title, DateTime? createdAtStart, DateTime? createdAtEnd)
        {
            try
            {
                var topics = await _topicService.SearchTopic(
                    page,
                    pageSize,
                    userId,
                    categoryId,
                    title,
                    createdAtStart,
                    createdAtEnd
                );

                return StatusCode(topics.StatusCode,topics); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while searching topics", details = ex.Message });
            }
        }

    }
}
