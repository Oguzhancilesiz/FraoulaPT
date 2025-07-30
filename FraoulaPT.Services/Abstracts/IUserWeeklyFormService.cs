using FraoulaPT.DTOs.UserWeeklyFormDTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IUserWeeklyFormService :
        IBaseService<UserWeeklyFormListDTO, UserWeeklyFormDetailDTO, UserWeeklyFormCreateDTO, UserWeeklyFormUpdateDTO>
    {
        Task<List<UserWeeklyFormListDTO>> GetListByUserAsync(Guid userId);
        Task<Guid> AddWithFilesAsync(UserWeeklyFormCreateDTO dto, Guid userId, List<IFormFile> files, string rootPath);
        Task<bool> UpdateWithFilesAsync(UserWeeklyFormUpdateDTO dto, List<IFormFile> newFiles, string rootPath);
        Task<UserWeeklyFormDetailDTO> GetDetailWithPhotosByIdAsync(Guid id);

        Task<UserWeeklyFormListDTO> GetLastFormByUserIdAsync(Guid userId);
        Task<List<UserWeeklyFormAdminListDTO>> GetAllForAdminAsync();

        Task<UserLastFormDTO> GetLastFormByUserAsync(Guid userId);

    }
}
