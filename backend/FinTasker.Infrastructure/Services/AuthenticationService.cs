using FinTasker.Application.Common.Exceptions;
using FinTasker.Application.Common.Interfaces.Service;
using FinTasker.Application.Common.Models;
using FinTasker.Domain.Entities;
using FinTasker.Domain.Enums;
using FinTasker.Infrastructure.Persistence;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FinTasker.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _config;

        public AuthenticationService(AppDbContext context, IJwtService jwtService, IConfiguration config)
        {
            _context = context;
            _jwtService = jwtService;
            _config = config;
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
                UserId = user.Id,
                Email = user.Email,
                Name = user.Name,
                AccessToken = token
            };
        }
        public async Task<AuthResult> AuthenticateWithGoogleAsync(
                    string idToken, CancellationToken ct = default)
        {
            // 1. Validasi Google token
            var clientId = _config["Authentication:Google:ClientId"];

            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(
                    idToken,
                    new GoogleJsonWebSignature.ValidationSettings
                    {
                        Audience = new[] { clientId }
                    });
            }
            catch (InvalidJwtException ex)
            {
                throw new UnauthorizedAccessException("Google token tidak valid.", ex);
            }

            // 2. Cari atau buat user 
            var user = await _context.Users
                .FirstOrDefaultAsync(
                    x => x.Provider == AuthProvider.Google &&
                         x.ProviderId == payload.Subject, ct);

            if (user == null)
            {
                user = new Users
                {
                    Id = Guid.NewGuid(),
                    Email = payload.Email,
                    Name = payload.Name,
                    AvatarUrl = payload.Picture,
                    Provider = AuthProvider.Google,
                    ProviderId = payload.Subject,
                    Role = Role.User,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Users.Add(user);
            }

            // 3. Update login timestamp
            user.LastLoginAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(ct);

            // 4. Generate token — identik dengan AuthenticateAsync
            var token = GenerateToken(user);

            return new AuthResult
            {
                UserId = user.Id,
                Email = user.Email,
                Name = user.Name
            };
        }

    }
}