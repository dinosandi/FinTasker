using FinTasker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinTasker.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Users> Users { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}