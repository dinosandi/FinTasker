using FinTasker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinTasker.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Users> Users { get; }
        DbSet<TaskActivities> TaskActivities  { get; }
        DbSet<Notifications> Notifications { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}