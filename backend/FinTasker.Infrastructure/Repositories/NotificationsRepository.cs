
using FinTasker.Application.Common.Interfaces;
using FinTasker.Application.Common.Interfaces.Repository;
using FinTasker.Domain.Entities;

namespace FinTasker.Infrastructure.Repositories
{
    public class NotificationsRepository : INotificationsRepository
    {
        private readonly IAppDbContext _context;

        public NotificationsRepository(IAppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Notifications notification, CancellationToken ct = default)
        {
            await _context.Notifications.AddAsync(notification, ct);
            await _context.SaveChangesAsync(ct);
        }
    }
}