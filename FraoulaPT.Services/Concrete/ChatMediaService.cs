using FraoulaPT.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using FraoulaPT.Entity;

namespace FraoulaPT.Services.Concrete
{
    public class ChatMediaService : IChatMediaService
    {
        private readonly IWebHostEnvironment _env;

        public ChatMediaService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<List<ChatMedia>> SaveMediaAsync(List<IFormFile> mediaFiles)
        {
            var savedMedia = new List<ChatMedia>();
            if (mediaFiles == null || mediaFiles.Count == 0)
                return savedMedia;

            var uploadRoot = Path.Combine(_env.WebRootPath, "uploads", "chat");
            if (!Directory.Exists(uploadRoot))
                Directory.CreateDirectory(uploadRoot);

            foreach (var file in mediaFiles)
            {
                if (file.Length > 10 * 1024 * 1024) // 10 MB sınırı
                    throw new Exception($"'{file.FileName}' dosyası çok büyük (maksimum 10MB)");

                var extension = Path.GetExtension(file.FileName).ToLower();
                var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif", ".mp4", ".pdf", ".docx", ".xlsx" };
                if (Array.IndexOf(allowed, extension) == -1)
                    throw new Exception($"'{file.FileName}' uzantısına izin verilmiyor.");

                var uniqueName = Guid.NewGuid().ToString() + extension;
                var filePath = Path.Combine(uploadRoot, uniqueName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                savedMedia.Add(new ChatMedia
                {
                    Id = Guid.NewGuid(),
                    FileName = file.FileName,
                    FileSize = file.Length,
                    MediaType = GetMediaType(extension),
                    MediaUrl = "/uploads/chat/" + uniqueName
                });
            }

            return savedMedia;
        }

        private string GetMediaType(string extension)
        {
            return extension switch
            {
                ".jpg" or ".jpeg" or ".png" or ".gif" => "image",
                ".mp4" => "video",
                ".pdf" or ".docx" or ".xlsx" => "file",
                _ => "file"
            };
        }
    }
}
