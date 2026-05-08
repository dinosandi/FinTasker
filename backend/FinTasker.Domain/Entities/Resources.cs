using System;
using FinTasker.Domain.Enums;


namespace FinTasker.Domain.Entities
{
    public class Resources
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public ResourceStatus Status { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        // Navigation property
        public ICollection<TaskResources> TaskResources { get; set; }
        
    }
}

