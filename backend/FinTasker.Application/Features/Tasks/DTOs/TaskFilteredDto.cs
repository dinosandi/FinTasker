using FinTasker.Domain.Enums;

namespace FinTasker.Application.Features.Tasks.DTOs
{
    // DTO utama task dengan semua relasi include
    public class TaskFilteredDto
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public DateTimeOffset? CompletedAt { get; set; }
        public int EstimatedMinutes { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        // --- Relations ---
        public List<TaskChecklistDto> Checklists { get; set; } = new();
        public List<TaskTagDto> Tags { get; set; } = new();
        public List<TaskTimeLogDto> TimeLogs { get; set; } = new();
        public List<TaskActivityDto> Activities { get; set; } = new();
        public List<PomodoroSessionDto> PomodoroSessions { get; set; } = new();
        public List<TaskResourceDto> Resources { get; set; } = new();

        // --- Computed Summary ---
        public int TotalChecklistItems { get; set; }
        public int CompletedChecklistItems { get; set; }
        public int TotalLoggedMinutes { get; set; }
        public int TotalPomodoroMinutes { get; set; }
    }

    public class TaskChecklistDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public DateTimeOffset? CompletedAt { get; set; }
    }

    public class TaskTagDto
    {
        public Guid TagId { get; set; }
        public string Name { get; set; }
        public string? Color { get; set; }
    }

    public class TaskTimeLogDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public int? DurationMinutes { get; set; }
        public string? Notes { get; set; }
    }

    public class TaskActivityDto
    {
        public Guid Id { get; set; }
        public ActivityType ActivityType { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }

    public class PomodoroSessionDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public int DurationMinutes { get; set; }
        public PomodoroSessionStatus SessionStatus { get; set; }
    }

    public class TaskResourceDto
    {
        public Guid Id { get; set; }
        public Guid ResourcesId { get; set; }
        public string? Notes { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
    }
   
}