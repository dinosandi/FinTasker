using MediatR;
using FinTasker.Domain.Enums;
using FinTasker.Application.Common.Models;
using FinTasker.Application.Features.Tasks.DTOs;

namespace FinTasker.Application.Features.Tasks.Commands.CreateTasks
{
    public class CreateTasksCommand : IRequest<ApiResponse<TaskDto>>
    {
        public Guid ProjectId { get; set; } // Relasi ke Projects
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskPriority Priority { get; set; } // e.g., "Low", "Medium", "High"
        public StatusTask Status { get; set; } // e.g., "To Do", "In Progress", "Done"
        public DateTimeOffset DueDate { get; set; }
        public int Estimed_Minutes { get; set; }

    }
}

