
namespace QuizWeb.Application.DTOs.Response
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T? Result { get; set; }
        public ApiResponse() { }
        public ApiResponse(int statusCode, string message, T? result)
        {
            StatusCode = statusCode;
            Message = message;
            Result = result;
        }
    }
}
