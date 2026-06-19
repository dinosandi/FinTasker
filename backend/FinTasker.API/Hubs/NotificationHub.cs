using System;
using FinTasker.Application.Common.Interfaces.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace FinTasker.API.Hubs
{
    [Authorize] 
    public class NotificationHub : Hub<INotificationHub>
    {
        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier; // dari JWT claim "sub" / NameIdentifier
            if (!string.IsNullOrEmpty(userId))
            {
                // Setiap user punya group sendiri → notif personal
                await Groups.AddToGroupAsync(
                    Context.ConnectionId,
                    $"user_{userId}");
            }

            await Clients.Caller.Connected($"Connected as user {userId}");
            await base.OnConnectedAsync();
        }


        
        /// Nanti dipakai saat fitur kolaborasi aktif.
        public async Task JoinProjectGroup(string projectId)
        {
            var userId = Context.UserIdentifier;
            if (string.IsNullOrEmpty(userId)) return;

            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                $"project_{projectId}");
        }

        /// Dipanggil client saat leave project page.
        public async Task LeaveProjectGroup(string projectId)
        {
            await Groups.RemoveFromGroupAsync(
                Context.ConnectionId,
                $"project_{projectId}");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            // SignalR otomatis remove dari semua group saat disconnect
            await base.OnDisconnectedAsync(exception);
        }
    }
}
