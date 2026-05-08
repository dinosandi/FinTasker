using System;

namespace FinTasker.Domain.Entities
{

    public class ProductivityReports
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateOnly Date { get; set; }
        public int TotalTasksCompleted { get; set; }
        public int TotalFokusMinutes { get; set; }
        public int TotalIdleMinutes { get; set; }
        public decimal ProductivityScore { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        // Navigation property
        public Users User { get; set; }
    }
}

