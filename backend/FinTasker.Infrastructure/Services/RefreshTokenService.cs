using System;
using FinTasker.Application.Common.Interfaces.Service;
using System.Security.Cryptography;
using FinTasker.Infrastructure.Persistence;
using FinTasker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace FinTasker.Infrastructure.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public RefreshTokenService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<RefreshTokens> GenerateRefreshTokenAsync(Guid userId, string? ipAddress = null)
        {
            var expireDays = int.Parse(_config["Jwt:RefreshExpireDays"] ?? "7");

            var refreshToken = new RefreshTokens
            {
                UsersId = userId,
                Token = GenerateSecureToken(),     // Token random 64 byte
                ExpiresAt = DateTime.UtcNow.AddDays(expireDays),
                CreatedByIp = ipAddress
            };

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return refreshToken;
        }

        public async Task<RefreshTokens?> GetValidTokenAsync(string token)
        {
            return await _context.RefreshTokens
                .Include(rt => rt.Users)
                .FirstOrDefaultAsync(rt => rt.Token == token && rt.IsActive);
        }

        public async Task RevokeTokenAsync(string token, string? ipAddress = null)
        {
            var refreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == token);

            if (refreshToken == null || !refreshToken.IsActive)
                throw new InvalidOperationException("Token is invalid or has been revoked."); //Token tidak valid atau sudah di-revoke

            refreshToken.RevokedAt = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;

            await _context.SaveChangesAsync();
        }

        public async Task RevokeAllUserTokensAsync(Guid userId, string? ipAddress = null)
        {
            var activeTokens = await _context.RefreshTokens
                .Where(rt => rt.UsersId == userId && rt.RevokedAt == null)
                .ToListAsync();

            foreach (var token in activeTokens)
            {
                token.RevokedAt = DateTime.UtcNow;
                token.RevokedByIp = ipAddress;
            }

            await _context.SaveChangesAsync();
        }

        // Generate token random yang aman secara kriptografis
        private static string GenerateSecureToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}


