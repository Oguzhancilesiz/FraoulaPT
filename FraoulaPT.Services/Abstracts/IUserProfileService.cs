using FraoulaPT.DTOs.UserProfileDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IUserProfileService : IBaseService<
     UserProfileListDTO,
     UserProfileDetailDTO,
     UserProfileCreateDTO,
     UserProfileUpdateDTO>
    {

        Task<UserProfileDetailDTO> GetByAppUserIdAsync(Guid appUserId);

    }
}
