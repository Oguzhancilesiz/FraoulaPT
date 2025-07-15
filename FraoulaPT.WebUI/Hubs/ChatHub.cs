using FraoulaPT.Services.Abstracts;
using Microsoft.AspNetCore.SignalR;

namespace FraoulaPT.WebUI.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatMessageService _chatService;

        public ChatHub(IChatMessageService chatService)
        {
            _chatService = chatService;
        }

        public async Task SendMessage(string receiverId, string text, List<IFormFile> mediaFiles)
        {
            var senderId = Context.UserIdentifier;
            var result = await _chatService.SendMessageAsync(
                Guid.Parse(senderId),
                Guid.Parse(receiverId),
                text,
                mediaFiles
            );

            // Gönderen ve alıcıya mesajı gönder
            await Clients.User(senderId).SendAsync("ReceiveMessage", result, true);
            await Clients.User(receiverId).SendAsync("ReceiveMessage", result, false);
        }
    }
}
