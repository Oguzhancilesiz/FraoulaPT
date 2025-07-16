using FraoulaPT.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IMediaService
    {
        // Tekli dosya yükle ve Media kaydını döndür
        Task<Media> SaveMediaAsync(IFormFile file);

        // Çoklu dosya yükle ve Media kaydı listesini döndür
        Task<List<Media>> SaveMediaRangeAsync(IEnumerable<IFormFile> files);

        // Mevcut media id ile Media getir
        Task<Media> GetMediaByIdAsync(Guid id);

        // Media sil (soft delete - status ile veya hard delete)
        Task DeleteMediaAsync(Guid id);

        // Belirli bir entity'ye ait media'ları getir (isteğe bağlı)
        Task<List<Media>> GetMediaByOwnerIdAsync(Guid ownerId);

        // (İsteğe bağlı) Media güncelle
        Task UpdateMediaAsync(Media media);
    }
}
