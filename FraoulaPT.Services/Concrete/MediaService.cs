using FraoulaPT.Core.Abstracts;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class MediaService : IMediaService
    {
        private readonly IUnitOfWork _uow;
        private readonly IHostEnvironment _env; // veya IWebHostEnvironment, projenin yapısına göre değişir

        public MediaService(IUnitOfWork uow, IHostEnvironment env)
        {
            _uow = uow;
            _env = env;
        }

        public async Task<Media> SaveMediaAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Dosya yüklenemedi!");

            // Benzersiz dosya ismi
            var ext = Path.GetExtension(file.FileName);
            var uniqueName = $"{Guid.NewGuid()}{ext}";
            var uploadPath = Path.Combine(_env.ContentRootPath, "wwwroot", "uploads", uniqueName);

            // Klasör var mı kontrol et, yoksa oluştur
            var dir = Path.GetDirectoryName(uploadPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            using (var stream = new FileStream(uploadPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var media = new Media
            {
                Url = $"/uploads/{uniqueName}",
                AltText = Path.GetFileNameWithoutExtension(file.FileName),
                MediaType = GetMediaType(ext), // Kendi dosya uzantısı tipine göre ayarla
                ThumbnailUrl = "null", // Thumbnail üretmek istiyorsan burada set et
                Status = FraoulaPT.Core.Enums.Status.Active,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            };

            await _uow.Repository<Media>().AddAsync(media);
            await _uow.SaveChangesAsync();

            return media;
        }

        public async Task<List<Media>> SaveMediaRangeAsync(IEnumerable<IFormFile> files)
        {
            var mediaList = new List<Media>();
            foreach (var file in files)
            {
                var media = await SaveMediaAsync(file);
                mediaList.Add(media);
            }
            return mediaList;
        }

        public async Task<Media> GetMediaByIdAsync(Guid id)
        {
            var query = await _uow.Repository<Media>().GetBy(x => x.Id == id && x.Status == FraoulaPT.Core.Enums.Status.Active);
            return await query.FirstOrDefaultAsync();
        }

        public async Task DeleteMediaAsync(Guid id)
        {
            var query = await _uow.Repository<Media>().GetBy(x => x.Id == id && x.Status == FraoulaPT.Core.Enums.Status.Active);
            var entity = await query.FirstOrDefaultAsync();
            if (entity == null)
                throw new Exception("Media bulunamadı!");

            // Soft delete önerilir!
            entity.Status = FraoulaPT.Core.Enums.Status.Deleted;
            entity.ModifiedDate = DateTime.UtcNow;
            await _uow.Repository<Media>().Update(entity);
            await _uow.SaveChangesAsync();

            // Eğer hard delete istiyorsan, aşağıdaki kodu kullan
            // await _uow.Repository<Media>().Delete(entity);
            // await _uow.SaveChangesAsync();
        }

        public async Task<List<Media>> GetMediaByOwnerIdAsync(Guid ownerId)
        {
            // Burada ownerId, entity (ör: UserWeeklyForm, UserProfile) ile ilişki kurduğun alanı belirtir.
            // Eğer media'da doğrudan ownerId yoksa, ilgili navigation'dan ulaşman gerekir.
            // Örneğin ProgressPhotos ile ilişki varsa, join ile çekebilirsin.
            // Şimdilik placeholder olarak:
            var query = await _uow.Repository<Media>().GetBy(x => x.Status == FraoulaPT.Core.Enums.Status.Active /* && x.OwnerId == ownerId */);
            return await query.ToListAsync();
        }

        public async Task UpdateMediaAsync(Media media)
        {
            await _uow.Repository<Media>().Update(media);
            await _uow.SaveChangesAsync();
        }

        // Yardımcı method: dosya uzantısından media tipi belirleme
        private FraoulaPT.Core.Enums.MediaType GetMediaType(string extension)
        {
            switch (extension.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                case ".png":
                case ".gif":
                    return FraoulaPT.Core.Enums.MediaType.Image;
                case ".mp4":
                case ".mov":
                case ".avi":
                    return FraoulaPT.Core.Enums.MediaType.Video;
                case ".pdf":
                case ".doc":
                case ".docx":
                    return FraoulaPT.Core.Enums.MediaType.Document;
                default:
                    return FraoulaPT.Core.Enums.MediaType.Other;
            }
        }
    }
}
