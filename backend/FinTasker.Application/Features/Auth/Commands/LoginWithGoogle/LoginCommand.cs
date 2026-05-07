using MediatR;
using FinTasker.Application.Common.Models;

namespace FinTasker.Application.Features.Auth.Commands.LoginWithGoogle
{
    public record LoginWithGoogle(string IdToken) : IRequest<ApiResponse<AuthResponse>>;
}