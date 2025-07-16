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
        /// <summary>
        /// Kullanıcının koç ID'sini alır.
        /// </summary>
        /// <returns></returns>
        Task<Guid> GetCoachIdAsync();

        /// <summary>
        /// Kullanıcı veya koç mesajı gönderir. Metin ve medya dosyaları birlikte gönderilebilir.
        /// Mesaj veya medya gönderildikçe paket hakkı düşer.
        /// </summary>
        /// <param name="senderId">Gönderen kullanıcı/koç ID</param>
        /// <param name="receiverId">Alıcı kullanıcı/koç ID</param>
        /// <param name="text">Mesaj metni</param>
        /// <param name="mediaFiles">Ekli dosyalar (isteğe bağlı)</param>
        /// <returns>Gönderilen mesajın DTO'su</returns>
        Task<ChatMessageDTO> SendMessageAsync(Guid senderId, Guid receiverId, string text, List<IFormFile> mediaFiles, string webRootPath);


        /// <summary>
        /// Kullanıcı ve koç arasındaki tüm mesajları getirir (tarihe göre sıralı).
        /// </summary>
        /// <param name="userId">Kullanıcı ID</param>
        /// <param name="coachId">Koç ID</param>
        /// <returns>Mesajların DTO listesi</returns>
        Task<List<ChatMessageDTO>> GetChatAsync(Guid userId, Guid coachId);

        /// <summary>
        /// Kullanıcının mevcut aktif paketinden kalan mesaj hakkı.
        /// </summary>
        /// <param name="userId">Kullanıcı ID</param>
        /// <returns>Kalan mesaj sayısı</returns>
        Task<int> GetRemainingMessageCount(Guid userId);

        /// <summary>
        /// Kullanıcının aktif paketinin ID'sini döner.
        /// </summary>
        /// <param name="userId">Kullanıcı ID</param>
        /// <returns>Paket ID</returns>
        Task<Guid> GetActivePackageId(Guid userId);
    }
}
