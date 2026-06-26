using FinTasker.Application.Common.Interfaces.Repository;
using FinTasker.Application.Common.Interfaces.Service;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Features.Tasks.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinTasker.Application.Features.Tasks.Queries.GetFilteredTasks
{
    public class GetFilteredTasksHandler
        : IRequestHandler<GetFilteredTasksQuery, ApiResponse<List<TaskFilteredDto>>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ITasksRepository _taskRepository;

        public GetFilteredTasksHandler(
            ICurrentUserService currentUserService,
            ITasksRepository taskRepository)
        {
            _currentUserService = currentUserService;
            _taskRepository = taskRepository;
        }

        public async Task<ApiResponse<List<TaskFilteredDto>>> Handle(
            GetFilteredTasksQuery request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId
                ?? throw new UnauthorizedAccessException("User is not authenticated.");

            // ── 1. Base query 
            var query = _taskRepository
                .GetQueryable()
                .AsNoTracking()
                .Where(t => t.Project.UsersId == userId);

            // ── 2. Dynamic filters 
            if (request.ProjectId.HasValue)
                query = query.Where(t => t.ProjectId == request.ProjectId.Value);

            if (request.Status.HasValue)
                query = query.Where(t => t.Status == request.Status.Value);

            if (request.Priority.HasValue)
                query = query.Where(t => t.Priority == request.Priority.Value);

            if (!string.IsNullOrWhiteSpace(request.Tag))
                query = query.Where(t =>
                    t.TaskTagRelations.Any(x => x.Tag.Name == request.Tag));

            if (!string.IsNullOrWhiteSpace(request.Search))
                query = query.Where(t =>
                    t.Title.Contains(request.Search) ||
                    t.Description.Contains(request.Search));

            // ── 3. Count sebelum pagination 
            var totalCount = await query.CountAsync(cancellationToken);

            // ── 4. Projection + Pagination 
            var items = await query
                .OrderBy(t => t.CreatedAt)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(t => new TaskFilteredDto
                {
                    Id = t.Id,
                    ProjectId = t.ProjectId,
                    ProjectName = t.Project.Name,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status.ToString(),
                    Priority = t.Priority.ToString(),
                    DueDate = t.DueDate,
                    EstimatedMinutes = t.Estimed_Minutes,
                    CompletedAt = t.CompletedAt,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt,

                    Checklists = t.TaskChecklists
                        .Select(c => new TaskChecklistDto
                        {
                            Id = c.Id,
                            Title = c.Title,
                            IsCompleted = c.IsCompleted,
                            CompletedAt = c.CompletedAt == default ? null : c.CompletedAt
                        })
                        .ToList(),

                    Tags = t.TaskTagRelations
                        .Select(tr => new TaskTagDto
                        {
                            TagId = tr.TagId,
                            Name = tr.Tag.Name,
                            Color = tr.Tag.Color
                        })
                        .ToList(),

                    TimeLogs = t.TaskTimeLogs
                        .Select(tl => new TaskTimeLogDto
                        {
                            Id = tl.Id,
                            StartTime = tl.StartTime,
                            EndTime = tl.EndTime,
                            DurationMinutes = tl.DurationMinutes,

                        })
                        .ToList(),

                    Activities = t.TaskActivities
                        .Select(a => new TaskActivityDto
                        {
                            Id = a.Id,
                            ActivityType = a.ActivityType.ToString(),
                            Description = a.Description,
                            CreatedAt = a.CreatedAt
                        })
                        .ToList(),

                    PomodoroSessions = t.PomodoroSession
                        .Select(p => new PomodoroSessionDto
                        {
                            Id = p.Id,
                            StartTime = p.StartTime,
                            EndTime = p.EndTime,
                            DurationMinutes = p.DurationMinutes,
                            SessionStatus = p.SessionStatus
                        })
                        .ToList(),

                    Resources = t.TaskResources
                        .Select(r => new TaskResourceDto
                        {
                            Id = r.Id,
                            ResourcesId = r.ResourcesId,
                            Notes = r.Notes,
                            StartTime = r.StartTime,
                            EndTime = r.EndTime
                        })
                        .ToList(),

                    // Computed — dihitung langsung di DB via SQL COUNT/SUM
                    TotalChecklistItems = t.TaskChecklists.Count(),
                    CompletedChecklistItems = t.TaskChecklists.Count(c => c.IsCompleted),
                    TotalLoggedMinutes = t.TaskTimeLogs.Sum(tl => (int?)tl.DurationMinutes) ?? 0,
                    TotalPomodoroMinutes = t.PomodoroSession.Sum(p => (int?)p.DurationMinutes) ?? 0
                })
                .ToListAsync(cancellationToken);

            return ApiResponse<List<TaskFilteredDto>>.Ok(
                items,
                "Successfully get filtered tasks."
            );
        }
    }
}