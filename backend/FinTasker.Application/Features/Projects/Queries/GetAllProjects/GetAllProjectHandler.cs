using Microsoft.EntityFrameworkCore;
using MediatR;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Common.Interfaces.Repository;
using FinTasker.Application.Common.Interfaces.Service;
using FinTasker.Application.Features.Projects.DTOs;


namespace FinTasker.Application.Features.Projects.Queries.GetAllProjects
{
    public class GetAllProjectHandler
        : IRequestHandler<GetAllProjectQuery, ApiResponse<List<ProjectDto>>>
    {
        private readonly IProjectsRepository _projectsRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetAllProjectHandler(
            IProjectsRepository projectsRepository,
            ICurrentUserService currentUserService)
        {
            _projectsRepository = projectsRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<List<ProjectDto>>> Handle(
            GetAllProjectQuery request,
            CancellationToken cancellationToken)
        {
            // cek apakah user login
            var userId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException("User is not logged in.");

            // Mulai query dengan filter berdasarkan userId
            var query = _projectsRepository
                .GetQueryable()
                .Where(p => p.UsersId == userId);

            // kondisi pencarian
            if (!string.IsNullOrWhiteSpace(request.Search))
                query = query.Where(p =>
                    p.Name.Contains(request.Search) ||
                    p.Description.Contains(request.Search));


            // filter status
            if (request.Status.HasValue)
                query = query.Where(p => p.Status == request.Status.Value);



            query = request.SortBy?.ToLower() switch
            {
                "name" => request.SortDescending
                                ? query.OrderByDescending(p => p.Name)
                                : query.OrderBy(p => p.Name),
                "createdat" => request.SortDescending
                                ? query.OrderByDescending(p => p.CreatedAt)
                                : query.OrderBy(p => p.CreatedAt),
                "status" => request.SortDescending
                                ? query.OrderByDescending(p => p.Status)
                                : query.OrderBy(p => p.Status),
                _ => query.OrderByDescending(p => p.CreatedAt)
            };

            var totalCount = await query.CountAsync(cancellationToken);  // ← nama konsisten

            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(p => new ProjectDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Status = p.Status,
                    Color = p.Color,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                })
                .ToListAsync(cancellationToken);
            var meta = new PaginationMeta
            {
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize),
                HasNextPage =
        request.Page * request.PageSize < totalCount,
                HasPreviousPage = request.Page > 1
            };

            return ApiResponse<List<ProjectDto>>
            .SuccessResponse(
                items,
                meta,
                "Projects successfully fetched.");
        }
    }
}

