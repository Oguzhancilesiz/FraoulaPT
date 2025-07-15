using FraoulaPT.DTOs.ChatMediaDTO;
using FraoulaPT.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IChatMediaService
    {
        Task<List<ChatMedia>> SaveMediaAsync(List<IFormFile> mediaFiles);
    }
}
