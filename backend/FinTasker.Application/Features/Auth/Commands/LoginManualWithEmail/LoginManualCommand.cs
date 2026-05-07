using System;
using FinTasker.Application.Common.Models;
using MediatR;


namespace FinTasker.Application.Features.Auth.Commands.LoginManualWithEmail
{
    public class LoginManualCommand : IRequest<ApiResponse<AuthResponse>>
    {
        public string Email { get; set; }
        public string? PasswordHash { get; set; }

    }

}
 