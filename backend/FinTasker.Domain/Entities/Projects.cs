using System;

namespace FinTasker.Domain.Entities
{
    public class Projects
    {
        public Guid Id { get; set; }
        public Guid UsersId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Color { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        // Navigation property
        public Users User { get; set; }

        // untuk relasi one-to-many dengan Tasks
        public ICollection<Tasks> Tasks { get; set; } // untuk daftar task dalam proyek

        // untuk relasi one-to-many dengan TaskMilestones
        public ICollection<TaskMilestones> TaskMilestones { get; set; } // untuk daftar milestone dalam proyek
        

    }
}

