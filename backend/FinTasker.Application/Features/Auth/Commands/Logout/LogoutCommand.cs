using System;
using MediatR;

namespace FinTasker.Application.Features.Auth.Commands.Logout
{
    public record LogoutCommand : IRequest;
}

