using MediatR;
using FinTasker.Application.Common.Models;

namespace FinTasker.Application.Features.Auth.Commands.Login
{
    public record LoginCommand(string IdToken) : IRequest<ApiResponse<AuthResponse>>;
}