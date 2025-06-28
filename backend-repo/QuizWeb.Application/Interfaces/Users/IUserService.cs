using QuizWeb.Application.DTOs.Response;
using QuizWeb.Application.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWeb.Application.Interfaces.Users
{
    public interface IUserService
    {
        Task<ApiResponse<AuthResponse>> RegisterAsync(RegisterRequest request);
        Task<ApiResponse<AuthResponse>> LoginAsync(LoginRequest request);
        Task<ApiResponse<AuthResponse>> EnterTestAsync(string displayName, int testId);
    }

}
