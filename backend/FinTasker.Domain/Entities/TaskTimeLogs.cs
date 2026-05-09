using System;

namespace FinTasker.Domain.Entities
{
    public class TaskTimeLogs // untuk tracking waktu
    {
        public Guid Id { get; set; }
        public Guid TasksId { get; set; }
        public DateTimeOffset StartTime { get; set; } // untuk start time
        public DateTimeOffset? EndTime { get; set; } // untuk end time, nullable karena mungkin belum selesai
        public int DurationMinutes { get; set; } // durasi dalam menit
        public string Description { get; set; }
        public DateTimeOffset CreadedAt { get; set; }

        // Navigation property
        public Tasks Tasks { get; set; }
    }
}

