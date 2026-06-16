using FinTasker.Domain.Enums;

namespace FinTasker.Application.Features.Tasks.DTOs
{
    public record TaskDto
    {
        public Guid Id { get; set; } // Relasi ke Projects
        public Guid ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public DateTimeOffset DueDate { get; set; } // bisa null kalau tidak ada deadline
        public DateTimeOffset CompletedAt { get; set; }
        public int Estimed_Minutes { get; set; }
    }
}

