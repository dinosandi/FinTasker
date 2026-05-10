using System;

namespace FinTasker.Domain.Entities
{
    public class RefreshTokens
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UsersId { get; set; }

        public string Token { get; set; } = string.Empty;

        //  token ini expired
        public DateTime ExpiresAt { get; set; }

        //  token dibuat
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //  token di-revoke (null = masih aktif)
        public DateTime? RevokedAt { get; set; }

        // IP address yang membuat token (opsional, untuk keamanan saja)
        public string? CreatedByIp { get; set; }

        // IP address yang revoke token (opsional)
        public string? RevokedByIp { get; set; }

        // Computed property: apakah token masih aktif?
        public bool IsActive => RevokedAt == null && DateTime.UtcNow < ExpiresAt;
        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        public bool IsRevoked => RevokedAt != null;

        // Navigation property
        public Users Users { get; set; } = null!;
    }


}

