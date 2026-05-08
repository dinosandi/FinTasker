using FinTasker.Domain.Enums;

namespace FinTasker.Domain.Entities
{

    public class Tasks
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public StatusTask Status { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTimeOffset DueDate { get; set; } // bisa null kalau tidak ada deadline
        public DateTimeOffset CompletedAt { get; set; }
        public int Estimed_Minutes { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        // Navigation property
        public Projects Project { get; set; }

        // untuk relasi one-to-many dengan TaskTimeLogs
        public ICollection<TaskTimeLogs> TaskTimeLogs { get; set; } // untuk log waktu kerja pada task
        public ICollection<TaskActivities> TaskActivities { get; set; } // untuk checklist dalam task
        public ICollection<TaskChecklists> TaskChecklists { get; set; } // untuk checklist dalam task
        public ICollection<PomodoroSession> PomodoroSession { get; set; } // untuk milestone dalam task
        public ICollection<TaskTagRelations> TaskTagRelations { get; set; } // untuk milestone dalam task
        public ICollection<TaskResources> TaskResources { get; set; } // untuk milestone dalam task


    }
}

