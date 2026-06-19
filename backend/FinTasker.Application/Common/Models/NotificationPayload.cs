
namespace FinTasker.Application.Common.Models
{

    public class NotificationPayload
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string TargetType { get; set; }
        public Guid TargetId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

    }
}
