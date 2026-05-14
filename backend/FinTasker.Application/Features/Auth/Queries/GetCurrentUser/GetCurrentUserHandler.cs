using MediatR;
using FinTasker.Application.Common.Exceptions;
using FinTasker.Application.Common.Interfaces.Repository;
using FinTasker.Application.Common.Interfaces.Service;
using FinTasker.Application.Common.Models;

namespace FinTasker.Application.Features.Auth.Queries.GetCurrentUser;

public class GetCurrentUserHandler
    : IRequestHandler<GetCurrentUserQuery, ApiResponse<CurrentUserDto>>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserRepository _userRepository;

    public GetCurrentUserHandler(
        ICurrentUserService currentUserService,
        IUserRepository userRepository)
    {
        _currentUserService = currentUserService;
        _userRepository = userRepository;
    }

    public async Task<ApiResponse<CurrentUserDto>> Handle(
        GetCurrentUserQuery request,
        CancellationToken cancellationToken)
    {
        // Ambil userId dari JWT yang ada di HTTP-only cookie
        var userId = _currentUserService.UserId
            ?? throw new UnauthorizedAccessException("Token tidak valid atau sudah expired.");

        // Mencari user di database
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken)
            ?? throw new NotFoundException($"User dengan ID {userId} tidak ditemukan");

        var dto = new CurrentUserDto(
            user.Id,
            user.Name,
            user.Email,
            user.Role.ToString(),
            user.Provider.ToString(),
            user.AvatarUrl
        );

        return ApiResponse<CurrentUserDto>.SuccessResponse(dto);
    }
}