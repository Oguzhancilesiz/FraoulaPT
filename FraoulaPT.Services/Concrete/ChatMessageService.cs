using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.ChatMessageDTO;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Microsoft.AspNetCore.Http;
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
        private readonly IChatMediaService _mediaService;

        public ChatMessageService(IUnitOfWork unitOfWork, IChatMediaService mediaService)
        {
            _unitOfWork = unitOfWork;
            _mediaService = mediaService;
        }

        public async Task<ChatMessageDTO> SendMessageAsync(Guid senderId, Guid receiverId, string text, List<IFormFile> mediaFiles)
        {
            var package = await _unitOfWork.Repository<UserPackage>()
                .GetBy(x => x.AppUserId == senderId && x.IsActive && x.EndDate > DateTime.Now)
                .ContinueWith(t => t.Result.Include(x => x.Package).OrderByDescending(x => x.StartDate).FirstOrDefault());

            if (package == null)
                throw new Exception("Aktif paket bulunamadı.");

            int mediaCount = mediaFiles?.Count ?? 0;
            int messageQuota = package.Package.MaxMessagesPerPeriod;
            int used = package.UsedMessages;
            int willUse = (string.IsNullOrWhiteSpace(text) ? 0 : 1) + mediaCount;

            if (used + willUse > messageQuota)
                throw new Exception("Mesaj hakkınız doldu.");

            var mediaEntities = await _mediaService.SaveMediaAsync(mediaFiles);

            var message = new ChatMessage
            {
                Id = Guid.NewGuid(),
                UserPackageId = package.Id,
                SenderId = senderId,
                ReceiverId = receiverId,
                MessageText = text,
                SentAt = DateTime.Now,
                IsRead = false,
                MediaFiles = mediaEntities
            };

            await _unitOfWork.Repository<ChatMessage>().AddAsync(message);

            package.UsedMessages += willUse;
            await _unitOfWork.Repository<UserPackage>().UpdateAsync(package);

            await _unitOfWork.SaveChangesAsync();

            return new ChatMessageDTO
            {
                Id = message.Id,
                Text = text,
                SentAt = message.SentAt,
                SenderId = senderId,
                ReceiverId = receiverId,
                MediaFiles = mediaEntities.Select(m => new ChatMediaDTO
                {
                    FileName = m.FileName,
                    FileSize = m.FileSize,
                    MediaType = m.MediaType,
                    MediaUrl = m.MediaUrl
                }).ToList()
            };
        }

        public async Task<List<ChatMessageDTO>> GetChatAsync(Guid userId, Guid coachId)
        {
            var messages = await _unitOfWork.Repository<ChatMessage>()
                .GetBy(x => (x.SenderId == userId && x.ReceiverId == coachId) || (x.SenderId == coachId && x.ReceiverId == userId))
                .Include(x => x.MediaFiles)
                .OrderBy(x => x.SentAt)
                .ToListAsync();

            return messages.Select(m => new ChatMessageDTO
            {
                Id = m.Id,
                Text = m.MessageText,
                SentAt = m.SentAt,
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                MediaFiles = m.MediaFiles?.Select(media => new ChatMediaDTO
                {
                    FileName = media.FileName,
                    FileSize = media.FileSize,
                    MediaType = media.MediaType,
                    MediaUrl = media.MediaUrl
                }).ToList()
            }).ToList();
        }

        public async Task<int> GetRemainingMessageCount(Guid userId)
        {
            var package = await _unitOfWork.Repository<UserPackage>()
                .GetBy(x => x.AppUserId == userId && x.IsActive && x.EndDate > DateTime.Now)
                .ContinueWith(t => t.Result.Include(x => x.Package).OrderByDescending(x => x.StartDate).FirstOrDefault());

            return package == null ? 0 : (package.Package.MaxMessagesPerPeriod - package.UsedMessages);
        }

        public async Task<Guid> GetActivePackageId(Guid userId)
        {
            var package = await _unitOfWork.Repository<UserPackage>()
                .GetBy(x => x.AppUserId == userId && x.IsActive && x.EndDate > DateTime.Now)
                .ContinueWith(t => t.Result.OrderByDescending(x => x.StartDate).FirstOrDefault());

            if (package == null)
                throw new Exception("Aktif paket bulunamadı.");

            return package.Id;
        }

        public async Task<Guid> GetCoachIdAsync()
        {
            var users = _unitOfWork.Repository<AppUser>().GetAll();
            foreach (var user in users)
            {
                var roles = await _unitOfWork.Repository<AppRole>().GetAll();
                // Bu kısmı kendi Role kontrol mekanizmana göre değiştir (Identity kullanıyorsan UserManager ile yap)
                if (roles.Any(r => r.Name == "Koç"))
                    return user.Id;
            }

            throw new Exception("Koç bulunamadı.");
        }
    }
}
