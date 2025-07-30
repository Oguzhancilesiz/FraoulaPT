using FraoulaPT.DTOs.ExtraPackageOptionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IExtraPackageOptionService
    {
        Task<List<ExtraPackageOptionListDTO>> GetAllAsync();
        Task<List<ExtraPackageOptionListDTO>> GetPublicListAsync();
        Task<ExtraPackageOptionUpdateDTO?> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(ExtraPackageOptionCreateDTO dto);
        Task<bool> UpdateAsync(ExtraPackageOptionUpdateDTO dto);
        Task<bool> SoftDeleteAsync(Guid id);
        Task<bool> ToggleActiveAsync(Guid id);
    }

}
