using System;
using FinTasker.Domain.Enums;

namespace FinTasker.Domain.Entities
{
    public class PomodoroSession
    {
        public Guid Id { get; set; }
        public Guid TasksId { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public int DurationMinutes { get; set; } // Duration in minutes
        public PomodoroSessionStatus SessionStatus { get; set; }

        // Navigation property
        public Tasks Tasks { get; set; }
    }
}

