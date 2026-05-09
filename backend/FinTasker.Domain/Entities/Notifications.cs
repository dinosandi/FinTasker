using System;

namespace FinTasker.Domain.Entities
{
    public class Notifications
    {
        public Guid Id { get; set; }
        public Guid UsersId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public bool IsRead { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        // Navigation property
        public Users Users { get; set; }
    }
}

