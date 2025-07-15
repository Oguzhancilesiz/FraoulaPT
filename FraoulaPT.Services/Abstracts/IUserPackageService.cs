using FraoulaPT.DTOs.ChatMessageDTO;
using FraoulaPT.DTOs.UserPackageDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IUserPackageService
    {
        Task<UserPackageListDTO> GetCurrentActivePackageAsync(Guid userId);
        Task<MyCoachChatDTO> GetMyCoachInfoAsync(Guid userId);
    }
}
