using System;
using FinTasker.Application.Common.Models;
using MediatR;


namespace FinTasker.Application.Features.Auth.Commands.LoginManualWithEmail
{
    public record LoginManualCommand(
        string Email,
        string Password
    ) : IRequest<ApiResponse<AuthResponse>>;
}

 