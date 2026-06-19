using FinTasker.Application.Common.Interfaces.Hubs;
using FinTasker.Application.Common.Interfaces.Repository;
using FinTasker.Application.Common.Interfaces.Service;
using FinTasker.Application.Common.Models;
using FinTasker.Domain.Entities;
using FinTasker.Domain.Enums;

namespace FinTasker.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationsRepository _repo;
        private readonly INotificationHubClient   _hubClient; 

        public NotificationService(
            INotificationsRepository repo,
            INotificationHubClient hubClient)   
        {
            _repo      = repo;
            _hubClient = hubClient;
        }

        public async Task SendToUserAsync(
            Guid userId,
            string title,
            string message,
            NotificationType type,
            CancellationToken ct = default)
        {
            // 1. Persist ke database
            var notification = await PersistAsync(userId, title, message, type, ct);

            // 2. Kirim realtime 
            var payload = BuildPayload(notification, "user", userId);
            await _hubClient.SendToUserAsync(userId.ToString(), payload, ct);
        }

        public async Task SendToProjectAsync(
            Guid projectId,
            string title,
            string message,
            NotificationType type,
            CancellationToken ct = default)
        {
            var payload = new NotificationPayload
            {
                Id         = Guid.NewGuid(),
                Title      = title,
                Message    = message,
                Type       = type.ToString(),
                TargetType = "project",
                TargetId   = projectId,
                CreatedAt  = DateTimeOffset.UtcNow
            };

            await _hubClient.SendToProjectAsync(projectId.ToString(), payload, ct);
        }

        // ── Private helpers 

        private async Task<Notifications> PersistAsync(
            Guid userId, string title, string message,
            NotificationType type, CancellationToken ct)
        {
            var notification = new Notifications
            {
                Id        = Guid.NewGuid(),
                UsersId   = userId,
                Title     = title,
                Message   = message,
                Type      = type.ToString(),
                IsRead    = false,
                CreatedAt = DateTimeOffset.UtcNow
            };

            await _repo.AddAsync(notification, ct);
            return notification;
        }

        private static NotificationPayload BuildPayload(
            Notifications n, string targetType, Guid targetId) => new()
        {
            Id         = n.Id,
            Title      = n.Title,
            Message    = n.Message,
            Type       = n.Type.ToString(),
            TargetType = targetType,
            TargetId   = targetId,
            CreatedAt  = n.CreatedAt
        };
    }
}