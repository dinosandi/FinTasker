using MediatR;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using FinTasker.Application.Common.Models;
using FinTasker.Domain.Entities;
using FinTasker.Domain.Enums;
using Microsoft.Extensions.Configuration;
using FinTasker.Application.Common.Interfaces;

namespace FinTasker.Application.Features.Auth.Commands.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, ApiResponse<AuthResponse>>
    {
        private readonly IAppDbContext _context;
        private readonly IConfiguration _config;
        private readonly IJwtService _jwtService;

        public LoginHandler(IAppDbContext context, IConfiguration config, IJwtService jwtService)
        {    
            _jwtService = jwtService;
            _context = context;
            _config = config;
        }

        public async Task<ApiResponse<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Get ClientId dari appsettings
                var clientId = _config["Authentication:Google:ClientId"];

                // Validate Google Token
                var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { clientId }
                });

                // Cari user berdasarkan ProviderId
                var user = await _context.Users
                    .FirstOrDefaultAsync(x =>
                        x.Provider == AuthProvider.Google &&
                        x.ProviderId == payload.Subject);

                // Kalau belum ada → register otomatis
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
                        IsEmailVerified = true,
                        IsProfileCompleted = false,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    _context.Users.Add(user);
                }

                // Update login info
                user.LastLoginAt = DateTime.UtcNow;
                user.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync(cancellationToken);

                // Generate JWT 
                var token = _jwtService.GenerateToken(user);

                var response = new AuthResponse
                {
                    Token = token,
                    Email = user.Email,
                    Name = user.Name,
                    IsProfileCompleted = user.IsProfileCompleted
                };

                return ApiResponse<AuthResponse>.SuccessResponse(response, "Login Google berhasil");
            }
            catch (Exception ex)
            {
                return ApiResponse<AuthResponse>.Fail($"Login gagal: {ex.Message}");
            }
        }
    }
}