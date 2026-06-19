using FinTasker.Application.Common.Models;

namespace FinTasker.Application.Common.Interfaces.Hubs
{
    public interface INotificationHubClient
    {
        Task SendToUserAsync(string userId, NotificationPayload payload, CancellationToken ct = default);
        Task SendToProjectAsync(string projectId, NotificationPayload payload, CancellationToken ct = default);
    }
}