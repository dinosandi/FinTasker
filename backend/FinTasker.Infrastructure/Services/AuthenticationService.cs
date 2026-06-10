using FinTasker.Application.Common.Exceptions;
using FinTasker.Application.Common.Interfaces.Service;
using FinTasker.Application.Common.Models;
using FinTasker.Domain.Entities;
using FinTasker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinTasker.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppDbContext _context;
        private readonly IJwtService  _jwtService;

        public AuthenticationService(AppDbContext context, IJwtService jwtService)
        {
            _context    = context;
            _jwtService = jwtService;
        }

        public bool VerifyPassword(string password, string passwordHash)
            => BCrypt.Net.BCrypt.Verify(password, passwordHash);

        public string GenerateToken(Users users)
            => _jwtService.GenerateToken(users);

        public async Task<AuthResult> AuthenticateAsync(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || !VerifyPassword(password, user.PasswordHash))
                throw new UnauthorizedException("Email atau password salah.");

            var token = GenerateToken(user);

            return new AuthResult
            {
                UserId      = user.Id,
                Email       = user.Email,
                Name        = user.Name,
                AccessToken = token
            };
        }
    }
}