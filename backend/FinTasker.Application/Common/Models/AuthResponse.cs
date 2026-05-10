using System;

namespace FinTasker.Application.Common.Models
{
    public class AuthResponse
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public bool IsProfileCompleted { get; set; }
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }

}

