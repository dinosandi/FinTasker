namespace FinTasker.Application.Common.Interfaces.Service
{
    public interface INotificationService
    {
        // Kirim ke user spesifik (personal notification)
        Task SendToUserAsync(
            Guid userId,
            string title,
            string message,
            NotificationType type,
            CancellationToken ct = default);

        // Kirim ke semua member dalam project (kolaborasi) tahap pengembangan
        Task SendToProjectAsync(
            Guid projectId,
            string title,
            string message,
            NotificationType type,
            CancellationToken ct = default);
    }
}