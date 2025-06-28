
namespace QuizWeb.Application.DTOs.Response
{
    public class PagedResponse<T>:ApiResponse<List<T>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }  
        public int TotalPage { get; set; }
        public PagedResponse() { }
        public PagedResponse(List<T> result, int page, int pageSize, int totalItemCount, string message, int statusCode)
            : base(statusCode, message, result)
        {
            Page = page;
            PageSize = pageSize;
            TotalPage = (int)Math.Ceiling(totalItemCount / (double)pageSize);
        }

        public static PagedResponse<T> Create(List<T> result, int page, int pageSize, int totalItemCount, string message, int statusCode)
        {
            var totalPage = (int)Math.Ceiling(totalItemCount / (double)pageSize);
            return new PagedResponse<T>(result, page, pageSize, totalPage, message, statusCode);
        }
    }
}
