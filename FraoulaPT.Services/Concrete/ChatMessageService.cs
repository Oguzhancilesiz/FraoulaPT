using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.ChatMessageDTO;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly IChatMediaService _chatMediaService;

        public ChatMessageService(IUnitOfWork unitOfWork, IChatMediaService chatMediaService, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _chatMediaService = chatMediaService;
            _userManager = userManager;
        }
        public async Task<Guid> GetCoachIdAsync()
        {
            // Sistemde "Koç" rolündeki ilk kullanıcıyı bul
            var allUsers = _userManager.Users.ToList();
            var coach = allUsers.FirstOrDefault(u => _userManager.IsInRoleAsync(u, "KOC").Result);

            if (coach == null)
                throw new Exception("Sistemde Koç bulunamadı!");

            return coach.Id;
        }
        public async Task<ChatMessageDTO> SendMessageAsync(Guid senderId, Guid receiverId, string text, List<IFormFile> mediaFiles, string webRootPath)
        {
            var uploadRoot = Path.Combine(webRootPath, "uploads", "chat");

            // Kullanıcının aktif paketini bul
            var userPackageQuery = await _unitOfWork.Repository<UserPackage>().GetBy(x =>
                x.AppUserId == senderId && x.IsActive && x.EndDate > DateTime.Now);

            var activePackage = await userPackageQuery.Include(x => x.Package).FirstOrDefaultAsync();

            if (activePackage == null || activePackage.Package == null)
                throw new Exception("Aktif paket bulunamadı.");

            int used = activePackage.UsedMessages;
            int limit = activePackage.Package.MaxMessagesPerPeriod;
            int mediaCount = mediaFiles?.Count ?? 0;
            int totalNew = (string.IsNullOrWhiteSpace(text) ? 0 : 1) + mediaCount;

            if (used + totalNew > limit)
                throw new Exception("Mesaj hakkınız dolmuştur.");

            // Medyaları kaydet
            var uploadedMedia = await _chatMediaService.SaveMediaAsync(mediaFiles);

            // ChatMessage Id’sini önce oluştur
            var messageId = Guid.NewGuid();

            // Media’lara ChatMessageId ata
            foreach (var media in uploadedMedia)
                media.ChatMessageId = messageId;

            var chatMessage = new ChatMessage
            {
                Id = messageId,
                UserPackageId = activePackage.Id,
                SenderId = senderId,
                ReceiverId = receiverId,
                MessageText = text,
                SentAt = DateTime.Now,
                IsRead = false,
                Status = Status.Active, // enum'un neyse
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                // AutoID = 0, // eğer otomatik ise elleme!
                MediaFiles = uploadedMedia
            };

            await _unitOfWork.Repository<ChatMessage>().AddAsync(chatMessage);

            // Eğer media'ları ayrı tabloya insert gerekiyorsa, şunu da kullanabilirsin:
            // await _unitOfWork.Repository<ChatMedia>().AddRangeAsync(uploadedMedia);

            // Mesaj hakkını güncelle
            activePackage.UsedMessages += totalNew;
            await _unitOfWork.Repository<UserPackage>().Update(activePackage);

            await _unitOfWork.SaveChangesAsync();

            // DTO manual mapping
            return new ChatMessageDTO
            {
                Id = chatMessage.Id,
                SenderId = chatMessage.SenderId,
                ReceiverId = chatMessage.ReceiverId,
                UserPackageId = chatMessage.UserPackageId,
                MessageText = chatMessage.MessageText,
                SentAt = chatMessage.SentAt,
                IsRead = chatMessage.IsRead,
                MediaFiles = chatMessage.MediaFiles?.Select(m => new ChatMediaDTO
                {
                    Id = m.Id,
                    MediaUrl = m.MediaUrl,
                    MediaType = m.MediaType,
                    FileSize = m.FileSize,
                    FileName = m.FileName
                }).ToList()
            };
        }


        public async Task<List<ChatMessageDTO>> GetChatAsync(Guid userId, Guid coachId)
        {
            var chatQuery = await _unitOfWork.Repository<ChatMessage>().GetBy(x =>
                (x.SenderId == userId && x.ReceiverId == coachId) ||
                (x.SenderId == coachId && x.ReceiverId == userId));

            var messages = await chatQuery
                .Include(x => x.MediaFiles)
                .OrderBy(x => x.SentAt)
                .ToListAsync();

            return messages.Select(x => new ChatMessageDTO
            {
                Id = x.Id,
                SenderId = x.SenderId,
                ReceiverId = x.ReceiverId,
                UserPackageId = x.UserPackageId,
                MessageText = x.MessageText,
                SentAt = x.SentAt,
                IsRead = x.IsRead,
                MediaFiles = x.MediaFiles?.Select(m => new ChatMediaDTO
                {
                    Id = m.Id,
                    MediaUrl = m.MediaUrl,
                    MediaType = m.MediaType,
                    FileSize = m.FileSize,
                    FileName = m.FileName
                }).ToList()
            }).ToList();
        }

        public async Task<int> GetRemainingMessageCount(Guid userId)
        {
            var userPackageQuery = await _unitOfWork.Repository<UserPackage>().GetBy(x =>
                x.AppUserId == userId && x.IsActive && x.EndDate > DateTime.Now);

            var activePackage = await userPackageQuery.Include(x => x.Package).FirstOrDefaultAsync();

            if (activePackage == null || activePackage.Package == null)
                return 0;

            return activePackage.Package.MaxMessagesPerPeriod - activePackage.UsedMessages;
        }

        public async Task<Guid> GetActivePackageId(Guid userId)
        {
            var userPackageQuery = await _unitOfWork.Repository<UserPackage>().GetBy(x =>
                x.AppUserId == userId && x.IsActive && x.EndDate > DateTime.Now);

            var activePackage = await userPackageQuery.FirstOrDefaultAsync();
            return activePackage?.Id ?? Guid.Empty;
        }
    }
}
