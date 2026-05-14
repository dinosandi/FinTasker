using MediatR;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Features.Tasks.Commands.Command;
using FinTasker.Application.Common.Interfaces.Service;



namespace FinTasker.Application.Features.Tasks.Commands.Handler
{
    public class CreateTasksHandler
        : IRequestHandler<CreateTasksCommand, ApiResponse<CreateTasksResponse>>

    {
        private readonly ITasksService _tasksService;
        private readonly IProjectsService _projectsService;

        public CreateTasksHandler(ITasksService tasksService, IProjectsService projectsService)
        {
            _tasksService = tasksService;
            _projectsService = projectsService;
        }

        public async Task<ApiResponse<CreateTasksResponse>> Handle(
            CreateTasksCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var project = await _projectsService.GetProjectsByIdAsync(request.ProjectId);
                if (project == null)
                {
                    return new ApiResponse<CreateTasksResponse>
                    {
                        Success = false,
                        Message = "Project not found"
                    };
                }


                var newTasks = new Domain.Entities.Tasks
                {
                    ProjectId = request.ProjectId,
                    Title = request.Title,
                    Description = request.Description,
                    Status = request.Status,
                    Priority = request.Priority,
                    DueDate = request.DueDate,
                    CompletedAt = request.CompletedAt,
                    Estimed_Minutes = request.Estimed_Minutes,
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                };

                await _tasksService.CreateTasksAsync(newTasks);

                var response = new CreateTasksResponse
                {
                    ProjectId = newTasks.ProjectId,
                    Title = newTasks.Title,
                    Description = newTasks.Description,
                    DueDate = newTasks.DueDate,
                    Status = newTasks.Status,
                    Priority = newTasks.Priority
                };

                return new ApiResponse<CreateTasksResponse>
                {
                    Success = true,
                    Message = "Task created successfully",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                // Log error (ex) jika perlu
                return new ApiResponse<CreateTasksResponse>
                {
                    Success = false,
                    Message = "Failed to create task: " + ex.Message
                };
            }
        }
    }
}

