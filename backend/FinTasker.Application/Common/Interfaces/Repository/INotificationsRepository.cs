using System;
using FinTasker.Domain.Entities;

namespace FinTasker.Application.Common.Interfaces.Repository
{

    public interface INotificationsRepository
    {
        Task AddAsync(Notifications notifications, CancellationToken ct = default);
    }
}
