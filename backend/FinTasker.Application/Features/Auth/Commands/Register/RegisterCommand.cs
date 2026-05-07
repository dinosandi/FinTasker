using System;
using MediatR;
using FinTasker.Domain.Enums;
using FinTasker.Application.Common.Models;

namespace FinTasker.Application.Features.Auth.Commands.Register
{

    public class RegisterCommand : IRequest<ApiResponse<AuthResponse>>
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set; }

    }
}

