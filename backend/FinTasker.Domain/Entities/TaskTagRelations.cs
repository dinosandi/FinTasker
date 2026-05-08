using System;

namespace FinTasker.Domain.Entities
{

    public class TaskTagRelations
    {
        public Guid Id { get; set; }
        public Guid TasksId { get; set; }
        public Guid TagId { get; set; }

        // Navigation properties
        public Tasks Tasks { get; set; }
        public TaskTags Tag { get; set; }
        
        
    }
}

