using FraoulaPT.DTOs.PackageDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IPackageService
    {
        Task<List<PackageListDTO>> GetActivePackagesAsync();
        Task<PackageListDTO> GetByIdAsync(Guid id);
        Task AddAsync(PackageCreateDTO dto);
        Task UpdateAsync(PackageEditDTO dto);
        Task DeleteAsync(Guid id);
    }
}
