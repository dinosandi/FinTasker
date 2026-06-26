using System;
using FinTasker.Application.Common.Models;
using FinTasker.Domain.Entities;


namespace FinTasker.Application.Common.Interfaces.Service
{
    public interface IAuthenticationService
    {
        bool VerifyPassword(string password, string passwordHash);
        string GenerateToken(Users users);
        Task<AuthResult> AuthenticateAsync(string email, string password);
        Task<AuthResult> AuthenticateWithGoogleAsync(string IdToken, CancellationToken ct= default);
    }
}

