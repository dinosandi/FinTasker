using System;
using FinTasker.Domain.Enums;

namespace FinTasker.Domain.Entities
{
    public class Users
    {
        public Guid Id { get; set; }

        // Authentication
        public string Email { get; set; }
        public string? PasswordHash { get; set; } // null kalau Google login
        public AuthProvider Provider { get; set; } // Local / Google
        public string? ProviderId { get; set; } // Google Sub ID

        // About Info
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? AvatarUrl { get; set; }

        //  Contact & Profile
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }

        // Business Logic
        public Role Role { get; set; }
        public bool IsProfileCompleted { get; set; }

        //  Security
        public bool IsEmailVerified { get; set; }
        public DateTime? LastLoginAt { get; set; }

        // Audit
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}