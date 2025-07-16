using FraoulaPT.Services.Abstracts;
using Microsoft.AspNetCore.SignalR;

namespace FraoulaPT.WebUI.Hubs
{
    public class ChatHub : Hub
    {
        // Anlık mesaj (hem kullanıcıya hem koça gitsin)
        public async Task SendMessage(string userId, string coachId, object messageDto)
        {
            await Clients.User(userId).SendAsync("ReceiveMessage", messageDto, true);   // Gönderen
            await Clients.User(coachId).SendAsync("ReceiveMessage", messageDto, false);  // Alan
        }
    }
}
