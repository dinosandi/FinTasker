using System;

namespace FinTasker.Domain.Entities
{

    public class TaskTags
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        // Navigation property
        public ICollection<TaskTagRelations> TaskTagRelations { get; set; }

    }
}

