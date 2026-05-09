
using FinTasker.Domain.Entities;

namespace FinTasker.Application.Common.Interfaces.Service
{
    public interface IJwtService
    {
        string GenerateToken(Users user);
    }
}
