using MediatR;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Common.Interfaces;
using FinTasker.Domain.Entities;
using FinTasker.Application.Common.Exceptions;


namespace FinTasker.Application.Features.Auth.Commands.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, ApiResponse<AuthResponse>>
    {
        // private readonly IAppDbContext _context;  // jika tidak memakai repository pattern, langsung inject dbcontext

        private readonly IUserRepository _userRepository; // jika memakai repository pattern, inject repository untuk dari segi performa sama

        public RegisterHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<AuthResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // validasi ketika create
            var exitingEmail = await _userRepository.GetUserByEmail(request.Email);
            if (exitingEmail != null)
            {
                throw new BadRequestException("Email already exists");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.PasswordHash);

            // Buat entity user baru
            var newUser = new Users
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                PasswordHash = passwordHash,
                Role = request.Role,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _userRepository.AddUserAsync(newUser);
            await _userRepository.SaveChangesAsync();

            var response = new AuthResponse
            {
                Name = newUser.Name,
                Email = newUser.Email,

            };
            return new ApiResponse<AuthResponse>
            {
                Success = true,
                Message = "Registration successful",
                Data = response
            };
        }
    }
}

