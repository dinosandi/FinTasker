using System;

namespace FinTasker.Domain.Entities
{
    public class TaskResources
    {
        public Guid Id { get; set; }
        public Guid TasksId { get; set; }
        public Guid ResourcesId { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public string Notes { get; set; }

        // Navigation properties
        public Tasks Tasks { get; set; }
        public Resources Resources { get; set; }

    }
}

