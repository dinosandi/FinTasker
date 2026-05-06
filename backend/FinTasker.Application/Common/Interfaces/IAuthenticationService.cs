using System;
using FinTasker.Domain.Entities;


namespace FinTasker.Application.Common.Interfaces
{
    public interface IAuthenticationService
    {
        bool VerifyPassword(string password, string passwordHash);
        string GenerateToken(Users users);
    }
}

