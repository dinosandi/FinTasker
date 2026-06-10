namespace FinTasker.Application.Common.Models
{
 
    public class AuthResult
    {
        public Guid UserId { get; init; }
        public string Email { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public string AccessToken { get; init; } = string.Empty;
    }
}
