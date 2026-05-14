using MediatR;
using FinTasker.Application.Common.Models;

namespace FinTasker.Application.Features.Auth.Commands.RefreshToken
{
    public record RefreshTokenCommand(string Token) : IRequest<AuthResponse>;
}