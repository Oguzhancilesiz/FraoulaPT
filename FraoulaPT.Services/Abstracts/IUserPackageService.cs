using FraoulaPT.DTOs.UserPackageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{

    public interface IUserPackageService
    {
        Task<bool> CreateAsync(UserPackageCreateDTO dto);
        Task<bool> HasActivePackageAsync(Guid userId);
        Task <List<UserPackageDetailDTO>> GetPackagesByUserAsync(Guid userId);
    }

}
