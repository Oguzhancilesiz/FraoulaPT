using FraoulaPT.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IUserProfileService
    {
        Task<ProfileViewDTO> GetProfileAsync(Guid appUserId);
        Task<ProfileEditDTO> GetProfileForEditAsync(Guid appUserId);
        Task UpdateProfileAsync(Guid appUserId, ProfileEditDTO dto);
    }
}
