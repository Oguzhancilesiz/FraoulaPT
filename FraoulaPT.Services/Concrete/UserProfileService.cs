using FraoulaPT.Core.Abstracts;
using FraoulaPT.DTOs.UserProfileDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class UserProfileService : BaseService<
    UserProfile,
    UserProfileListDTO,
    UserProfileDetailDTO,
    UserProfileCreateDTO,
    UserProfileUpdateDTO>, IUserProfileService
    {
        public UserProfileService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<UserProfileDetailDTO> GetByAppUserIdAsync(Guid appUserId)
        {
            var entity = (await _unitOfWork.Repository<UserProfile>()
                .GetBy(x => x.AppUserId == appUserId)).FirstOrDefault();

            return entity?.Adapt<UserProfileDetailDTO>();
        }
    }

}
