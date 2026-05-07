using FinTasker.Application.Common.Interfaces;
using FinTasker.Domain.Entities;
using FinTasker.Infrastructure.Persistence;

namespace FinTasker.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthenticationService(AppDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        // implementasi VerifyPassword menggunakan BCrypt
        public bool VerifyPassword(string password, string passwordHash)
        {
            // implementasi verifikasi password,  menggunakan BCrypt
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
        
        // Implementasi GenerateToken menggunakan JwtService
        public string GenerateToken(Users users)
        {
            return _jwtService.GenerateToken(users);
        }

    }

}

