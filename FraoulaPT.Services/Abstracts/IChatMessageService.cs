using FraoulaPT.DTOs.ChatMessageDTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IChatMessageService
    {
        Task<ChatMessageDTO> SendMessageAsync(Guid senderId, Guid receiverId, string text, List<IFormFile> mediaFiles);
        Task<List<ChatMessageDTO>> GetChatAsync(Guid userId, Guid coachId);
        Task<int> GetRemainingMessageCount(Guid userId);
        Task<Guid> GetActivePackageId(Guid userId);
        Task<Guid> GetCoachIdAsync(); // Kullanıcı için sistemdeki ilk koçun ID’sini verir
    }
}
