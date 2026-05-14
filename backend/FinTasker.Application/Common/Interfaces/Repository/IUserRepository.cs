using System;
using FinTasker.Domain.Entities;


namespace FinTasker.Application.Common.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<Users?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Users> GetUserByEmail(string Email);
        Task<Users> GetUsersAsync(string email, string name);
        Task AddUserAsync(Users user);
        Task SaveChangesAsync();
    }
}

