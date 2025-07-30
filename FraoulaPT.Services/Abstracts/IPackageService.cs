using FraoulaPT.DTOs.PackageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IPackageService
    {
        Task<List<PackageListDTO>> GetAllAsync();
        Task<PackageDetailDTO> GetByIdAsync(Guid id);
        Task<bool> AddAsync(PackageCreateDTO dto);
        Task<bool> UpdateAsync(PackageUpdateDTO dto);
        Task<bool> SoftDeleteAsync(Guid id);
    }

}
