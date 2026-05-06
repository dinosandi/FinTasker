using System;
using FinTasker.Domain.Entities;


namespace FinTasker.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<Users> GetUserByEmail(string Email);
        Task<Users> GetUsersAsync(string email, string name);
        Task AddUserAsync(Users user);
        Task SaveChangesAsync();
    }
}

