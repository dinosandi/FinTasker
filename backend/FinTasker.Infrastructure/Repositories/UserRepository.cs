using FinTasker.Domain.Entities;
using FinTasker.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using FinTasker.Infrastructure.Persistence;


namespace FinTasker.Infrastructure.Repositories
{
    public class userRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public userRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Users> GetUserByEmail(string Email)
        {
            return await _context.Users
            .AsNoTracking() // Menambahkan AsNoTracking untuk meningkatkan performa saat hanya membaca data
            .FirstOrDefaultAsync(u => u.Email == Email);
        }
        public async Task AddUserAsync(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task<Users> GetUsersAsync(string email, string name)
        {
            var user = await _context.Users
            .AsNoTracking() // Menambahkan AsNoTracking untuk meningkatkan performa saat hanya membaca data
            .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                user = new Users
                {
                    Email = email,
                    Name = name,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }

            return user;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}

