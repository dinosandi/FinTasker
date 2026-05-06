using System;

namespace FinTasker.Application.Common.Models
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public bool IsProfileCompleted { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

    }

}

