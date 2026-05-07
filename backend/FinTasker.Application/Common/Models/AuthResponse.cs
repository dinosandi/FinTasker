using System;

namespace FinTasker.Application.Common.Models
{
    public class AuthResponse
    {
        public string Email { get; set; }
        public string Name { get; set; }
        
        public string Token { get; set; }
        public bool IsProfileCompleted { get; set; }
    }

}

