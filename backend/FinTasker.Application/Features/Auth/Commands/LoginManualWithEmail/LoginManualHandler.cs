using FinTasker.Application.Common.Exceptions;
using FinTasker.Application.Common.Interfaces;
using MediatR;
using FinTasker.Application.Common.Models;  


namespace FinTasker.Application.Features.Auth.Commands.LoginManualWithEmail
{
    public class LoginManualHandler : IRequestHandler<LoginManualCommand, ApiResponse<AuthResponse>>
    
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;

        public LoginManualHandler(IUserService userService, IAuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
        }

        public async Task<ApiResponse<AuthResponse>> Handle(LoginManualCommand request, CancellationToken cancellationToken)
        {
            var users = await _userService.GetUserByEmail(request.Email);

            //validasi jika user tidak ada di database atau tidak terdaftar dengan email tersebut
            if (users == null)
            {
                throw new NotFoundException("User not found");
            }

            //validasi jika password yang dimasukkan tidak sesuai dengan password yang ada di database
            if (!_authenticationService.VerifyPassword(request.PasswordHash, users.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            // generate token JWT
            var token = _authenticationService.GenerateToken(users);

            return new ApiResponse<AuthResponse>
            {
                Success = true,
                Message = "Login successful",
                Data = new AuthResponse
                {
                    Token = token,
                    IsProfileCompleted = users.IsProfileCompleted,
                }
            };
        }
    }
    

    }
