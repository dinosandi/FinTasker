using System;
using FinTasker.Domain.Enums;

namespace FinTasker.Domain.Entities
{
    public class TaskActivities
    {
        public Guid Id { get; set; }
        public Guid TasksId { get; set; }
        public ActivityType ActivityType { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        // Navigation property
        public Tasks Tasks { get; set; }

    }
}

