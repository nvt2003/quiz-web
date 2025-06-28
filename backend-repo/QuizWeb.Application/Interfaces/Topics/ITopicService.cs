
using QuizWeb.Application.DTOs.Response;
using QuizWeb.Application.DTOs.Topics;

namespace QuizWeb.Application.Interfaces.Topics
{
    public interface ITopicService
    {
        Task<TopicDto> CreateTopicAsync(CreateTopicRequest request);
        Task<bool> IsTopicTitleDuplicateAsync(int userId, string title);
        Task<TopicDto> UpdateTopicAsync(UpdateTopicRequest request);
        Task<PagedResponse<TopicDto>> GetTopicByUserId(int userId, int page, int pageSize);
        Task<PagedResponse<TopicDto>> GetTopic(int page, int pageSize);
        Task<ApiResponse<TopicVM>> GetTopicById(int topicId);
        Task<ApiResponse<bool>> DeleteTopic(int topicId);
        Task<PagedResponse<TopicDto>> SearchTopic(int page, int pageSize, 
            int? userId, int? categoryId, string? title, DateTime? createdAtStart, DateTime? createdAtEnd);
    }

}
