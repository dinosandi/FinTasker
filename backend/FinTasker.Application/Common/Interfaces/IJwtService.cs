
using FinTasker.Domain.Entities;

namespace FinTasker.Application.Common.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Users user);
    }
}
