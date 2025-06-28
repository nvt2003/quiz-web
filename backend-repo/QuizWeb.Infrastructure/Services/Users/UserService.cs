using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuizWeb.Application.DTOs.Response;
using QuizWeb.Application.DTOs.Users;
using QuizWeb.Application.Interfaces.Users;
using QuizWeb.Infrastructure.Persistence;
using QuizWeb.Infrastructure.Persistence.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuizWeb.Infrastructure.Services.Users
{
    public class UserService : IUserService
    {
        private readonly QuizDbContext _context;
        private readonly IConfiguration _config;

        public UserService(QuizDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<ApiResponse<AuthResponse>> RegisterAsync(RegisterRequest request)
        {
            //check duplicate username
            if (await _context.Users.AnyAsync(u => u.Username == request.Username && u.DeletedAt == null))
            {
                return new ApiResponse<AuthResponse>(400,"Username already exists", null);
            }

            var user = new User
            {
                Username = request.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                DisplayName = request.Username,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var response = GenerateToken(user);
            return new ApiResponse<AuthResponse>(200, "Registered successfully", response);
        }

        public async Task<ApiResponse<AuthResponse>> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username && u.DeletedAt == null);
            //check username password
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return new ApiResponse<AuthResponse>(401,"Invalid username or password", null);
            }

            var response = GenerateToken(user);
            return new ApiResponse<AuthResponse>(200, "Login successful", response);
        }

        private AuthResponse GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username ?? ""),
                new Claim("DisplayName", user.DisplayName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );

            return new AuthResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserId = user.Id,
                Username = user.Username!,
                DisplayName = user.DisplayName
            };
        }
        public async Task<ApiResponse<AuthResponse>> EnterTestAsync(string displayName, int testId)
        {
            var test = await _context.Tests
                .Include(t => t.Participants)
                .FirstOrDefaultAsync(t => t.Id == testId && t.EndTime > DateTime.Now);

            if (test == null)
            {
                return new ApiResponse<AuthResponse>(404, "Test not found or has ended", null);
            }

            var user = new User
            {
                DisplayName = displayName,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var participant = new Participant
            {
                UserId = user.Id,
                TestId = test.Id,
                Score = null
            };

            _context.Participants.Add(participant);
            await _context.SaveChangesAsync();

            var token = GenerateToken(user);
            return new ApiResponse<AuthResponse>(200, "Joined test successfully", token);
        }

    }

}
