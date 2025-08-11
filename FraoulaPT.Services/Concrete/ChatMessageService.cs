using FraoulaPT.Core.Abstracts;
using FraoulaPT.DTOs.ChatMessageDTOs;
using FraoulaPT.DTOs.DashboardDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
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
        private readonly IUnitOfWork _uow;
        private readonly IUserPackageService _userPackageService;

        public ChatMessageService(IUnitOfWork uow, IUserPackageService userPackageService)
        {
            _uow = uow;
            _userPackageService = userPackageService;
        }

        public async Task<ChatMessage> CreateAsync(Guid senderId, Guid receiverId, string messageText, Guid? userPackageId = null)
        {
            // Eğer userPackageId verilmişse, doğruluğunu kontrol et (isteğe bağlı)
            if (userPackageId.HasValue)
            {
                var activePackage = await _userPackageService.GetActivePackageStatusAsync(senderId);
                if (activePackage == null || activePackage.UserPackageId != userPackageId.Value)
                    throw new Exception("Geçerli bir aktif paket bulunamadı.");
            }

            var message = new ChatMessage
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                MessageText = messageText,
                SentAt = DateTime.Now,
                IsRead = false,
                UserPackageId = userPackageId, // Koç için null olabilir
                CreatedByUserId = senderId,
                UpdatedByUserId = senderId,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            };

            await _uow.Repository<ChatMessage>().AddAsync(message);
            await _uow.SaveChangesAsync();

            return message;
        }


        public async Task<List<ChatMessage>> GetChatHistoryAsync(Guid senderId, Guid receiverId)
        {
            var messages = await _uow.Repository<ChatMessage>()
                           .Query()
                           .Where(m =>
                               (m.SenderId == senderId && m.ReceiverId == receiverId) ||
                               (m.SenderId == receiverId && m.ReceiverId == senderId))
                           .OrderBy(m => m.SentAt)
                           .ToListAsync();
            return messages;

        }
        public async Task<List<StudentChatListDTO>> GetStudentsWhoMessagedCoachAsync(Guid coachId)
        {
            var query = await _uow.Repository<ChatMessage>()
                                 .Query()
                                 .Where(m => m.ReceiverId == coachId)
                                 .Include(m => m.Sender)
                                     .ThenInclude(u => u.Profile)
                                 .GroupBy(m => m.SenderId)
                                 .Select(g => g.FirstOrDefault().Sender)
                                 .ToListAsync();

            return query.Select(u => new StudentChatListDTO
            {
                Id = u.Id,
                FullName = u.FullName,
                ProfilePhotoUrl = u.Profile?.ProfilePhotoUrl ?? "/uploads/user-default.jpg"
            }).ToList();

        }
        public async Task<int> GetActiveChatsCountAsync()
        {
            return await _uow.Repository<ChatMessage>()
                .Query()
                .Select(c => c.Id)
                .Distinct()
                .CountAsync();
        }

        public async Task<string> GetAverageResponseTimeAsync()
        {
            // Ortalama yanıt süresi (dakika)
            var avgMinutes = await _uow.Repository<ChatMessage>()
                .Query()
                .AverageAsync(m => EF.Functions.DateDiffMinute(m.SentAt, m.ModifiedDate ?? DateTime.UtcNow));

            return $"{avgMinutes} dk";
        }
        public async Task<List<UserActivityDTO>> GetRecentActivitiesAsync(int limit)
        {
            var messages = await _uow.Repository<ChatMessage>()
                .Query()
                .Include(m => m.Sender)
                .OrderByDescending(m => m.SentAt)
                .Take(limit)
                .Select(m => new
                {
                    UserName = m.Sender.FullName,
                    SentAt = m.SentAt
                })
                .ToListAsync();

            // EF sorgusundan çıktıktan sonra GetTimeAgo uygula
            var activities = messages.Select(m => new UserActivityDTO
            {
                UserName = m.UserName,
                ActionDescription = "Yeni mesaj gönderdi",
                TimeAgo = GetTimeAgo(m.SentAt) // Artık in-memory çalışıyor
            }).ToList();

            return activities;
        }

        // Zaman farkını "x dakika önce" formatına çevirir
        private string GetTimeAgo(DateTime dateTime)
        {
            var ts = DateTime.UtcNow - dateTime;

            if (ts.TotalMinutes < 1)
                return "az önce";
            if (ts.TotalMinutes < 60)
                return $"{(int)ts.TotalMinutes} dakika önce";
            if (ts.TotalHours < 24)
                return $"{(int)ts.TotalHours} saat önce";
            if (ts.TotalDays < 7)
                return $"{(int)ts.TotalDays} gün önce";

            return dateTime.ToLocalTime().ToString("dd.MM.yyyy HH:mm");
        }

    }

}
