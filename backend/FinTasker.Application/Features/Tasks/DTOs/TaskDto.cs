using FinTasker.Domain.Enums;

namespace FinTasker.Application.Features.Tasks.DTOs
{
    public record TaskDto
    {
        public Guid ProjectId { get; set; } // Relasi ke Projects
        public string Title { get; set; }
        public string Description { get; set; }
        public StatusTask Status { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTimeOffset DueDate { get; set; } // bisa null kalau tidak ada deadline
        public DateTimeOffset CompletedAt { get; set; }
        public int Estimed_Minutes { get; set; }
    }
}

