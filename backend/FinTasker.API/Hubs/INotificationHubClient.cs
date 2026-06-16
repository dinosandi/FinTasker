
using FinTasker.Application.Common.Interfaces.Hubs;
using FinTasker.Application.Common.Models;
using Microsoft.AspNetCore.SignalR;

namespace FinTasker.API.Hubs
{
    public class NotificationHubClient : INotificationHubClient
    {
        private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;

        public NotificationHubClient(
            IHubContext<NotificationHub, INotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendToUserAsync(
            string userId,
            NotificationPayload payload,
            CancellationToken ct = default)
        {
            await _hubContext
                .Clients
                .Group($"user_{userId}")
                .ReceiveNotification(payload);
        }

        public async Task SendToProjectAsync(
            string projectId,
            NotificationPayload payload,
            CancellationToken ct = default)
        {
            await _hubContext
                .Clients
                .Group($"project_{projectId}")
                .ReceiveNotification(payload);
        }
    }
}