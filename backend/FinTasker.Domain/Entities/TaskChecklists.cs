using System;

namespace FinTasker.Domain.Entities
{
    public class TaskChecklists
    {
        public Guid Id { get; set; }
        public Guid TasksId { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public DateTimeOffset CompletedAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        // Navigation property
        public Tasks Tasks { get; set; }
    }
}

