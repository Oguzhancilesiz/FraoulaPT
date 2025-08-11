using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace FraoulaPT.WebUI.Hubs
{
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier; // Guid string
            if (!string.IsNullOrEmpty(userId))
                await Groups.AddToGroupAsync(Context.ConnectionId, $"user:{userId}");

            // Rol bazlı grup (opsiyonel)
            if (Context.User.IsInRole("Admin"))
                await Groups.AddToGroupAsync(Context.ConnectionId, "Admin");
            if (Context.User.IsInRole("Koc"))
                await Groups.AddToGroupAsync(Context.ConnectionId, $"Koc:{userId}");

            await base.OnConnectedAsync();
        }
    }
}
