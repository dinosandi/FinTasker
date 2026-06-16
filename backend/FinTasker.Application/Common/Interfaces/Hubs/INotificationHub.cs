using System;
using FinTasker.Application.Common.Models;

namespace FinTasker.Application.Common.Interfaces.Hubs
{

    public interface INotificationHub
    {
        Task ReceiveNotification(NotificationPayload payload);
        Task Connected(string message);
    }

}

