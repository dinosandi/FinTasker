using System;
using MediatR;
using FinTasker.Domain.Enums;
using FinTasker.Application.Common.Models;

namespace FinTasker.Application.Features.Tasks.Commands.Command
{
    public class CreateTasksCommand : IRequest<ApiResponse<CreateTasksResponse>>
    {
        public Guid ProjectId { get; set; } // Relasi ke Projects
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskPriority Priority { get; set; } // e.g., "Low", "Medium", "High"
        public StatusTask Status { get; set; } // e.g., "To Do", "In Progress", "Done"
        public DateTimeOffset DueDate { get; set; }
        public DateTimeOffset CompletedAt { get; set; }
        public int Estimed_Minutes { get; set; }

    }
    public class CreateTasksResponse
    {
        public Guid ProjectId { get; set; } // Relasi ke Projects
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public StatusTask Status { get; set; }
        public TaskPriority Priority { get; set; } // e.g., "Low", "Medium", "High"
    }

}

