using System;
using FinTasker.Domain.Entities;

namespace FinTasker.Application.Common.Interfaces.Service
{

    public interface IRefreshTokenService
    {
        Task<RefreshTokens> GenerateRefreshTokenAsync(Guid userId, string? ipAddress = null);
        Task<RefreshTokens?> GetValidTokenAsync(string token);
        Task RevokeTokenAsync(string token, string? ipAddress = null);
        Task RevokeAllUserTokensAsync(Guid userId, string? ipAddress = null);
    }

}

