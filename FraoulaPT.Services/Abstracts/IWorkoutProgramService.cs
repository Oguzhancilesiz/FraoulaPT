using FraoulaPT.DTOs.UserProgramDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IWorkoutProgramService
    {
        Task<WorkoutProgramDTO> GetByIdAsync(Guid id, Guid currentUserId);
        Task<List<WorkoutProgramDTO>> GetAllByUserIdAsync(Guid userId); // kullanıcının tüm programları
        Task<Guid> CreateAsync(WorkoutProgramDTO dto); // Koç programı oluşturuyor
        Task<bool> UpdateAsync(WorkoutProgramDTO dto);
        Task<bool> DeleteAsync(Guid id);
        // Extra: gün/gün program getirme vs.
    }
}
