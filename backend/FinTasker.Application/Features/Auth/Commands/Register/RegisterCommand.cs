using System;
using MediatR;
using FinTasker.Domain.Enums;

namespace FinTasker.Application.Features.Auth.Commands.Register
{

    public class RegisterCommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

    }
}

