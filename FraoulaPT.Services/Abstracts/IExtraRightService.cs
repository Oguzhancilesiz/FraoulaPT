
using FraoulaPT.DTOs.ExtraRightDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IExtraRightService
    {
        Task<List<ExtraRightListDTO>> GetAllAsync();
        Task<bool> AddAsync(ExtraRightAddDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
