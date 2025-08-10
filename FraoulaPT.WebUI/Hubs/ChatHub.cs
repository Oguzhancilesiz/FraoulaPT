using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace FraoulaPT.WebUI.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatMessageService _chatMessageService;
        private readonly IUserPackageService _userPackageService;

        public ChatHub(IChatMessageService chatMessageService, IUserPackageService userPackageService)
        {
            _chatMessageService = chatMessageService;
            _userPackageService = userPackageService;
        }

        public async Task SendMessage(string toUserId, string messageText)
        {
            if (string.IsNullOrWhiteSpace(messageText)) return;

            var fromUserIdStr = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(fromUserIdStr, out Guid senderId)) return;
            if (!Guid.TryParse(toUserId, out Guid receiverId)) return;

            var roles = Context.User?.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
            if (roles == null || roles.Count == 0) return;

            if (roles.Contains("Ogrenci"))
            {
                var userPackage = await _userPackageService.GetActivePackageStatusAsync(senderId);
                if (userPackage == null)
                {
                    await Clients.User(senderId.ToString())
                        .SendAsync("QuotaExceeded", "Aktif bir paketiniz bulunamadı.");
                    return;
                }

                var now = DateTime.UtcNow;
                if (userPackage.EndDate < now)
                {
                    await Clients.User(senderId.ToString())
                        .SendAsync("QuotaExceeded", "Paket süreniz dolmuş. Lütfen yeni bir paket satın alın.");
                    return;
                }

                if (userPackage.UsedMessages >= userPackage.TotalMessages)
                {
                    await Clients.User(senderId.ToString())
                        .SendAsync("QuotaExceeded", "Mesaj hakkınız doldu. Lütfen ek paket satın alın.");
                    return;
                }

                await _userPackageService.IncrementUsedMessageAsync(senderId);

                var chat = await _chatMessageService.CreateAsync(senderId, receiverId, messageText, userPackage.UserPackageId);

                var messageDto = new
                {
                    messageText = chat.MessageText,
                    sentAt = chat.SentAt.ToString("o")
                };

                await Clients.User(senderId.ToString()).SendAsync("ReceiveMessage", receiverId.ToString(), messageDto, true);
                await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", senderId.ToString(), messageDto, false);
            }
            else if (roles.Contains("Koc"))
            {
                // 🆕 Koç için paket kontrolü yok
                var chat = await _chatMessageService.CreateAsync(senderId, receiverId, messageText, null);

                var messageDto = new
                {
                    messageText = chat.MessageText,
                    sentAt = chat.SentAt.ToString("o")
                };

                await Clients.User(senderId.ToString()).SendAsync("ReceiveMessage", receiverId.ToString(), messageDto, true);
                await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", senderId.ToString(), messageDto, false);
            }
            else
            {
                await Clients.User(senderId.ToString())
                    .SendAsync("QuotaExceeded", "Bu özelliği kullanamazsınız.");
            }
        }


    }
}
