using FraoulaPT.DTOs.UserWeeklyFormDTO;
using FraoulaPT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IUserWeeklyFormService
    {
        // Kullanıcı yeni form ekler (progress photos Media olarak gelir)
        Task AddFormAsync(UserWeeklyFormCreateDTO dto, List<Media> progressPhotos);

        // Kullanıcıya ait tüm formlar (aylık/haftalık)
        Task<List<UserWeeklyFormDTO>> GetUserFormsAsync(Guid userPackageId);

        // Hoca form feedback ekler
        Task AddCoachFeedbackAsync(UserWeeklyFormCoachFeedbackDTO dto);
    }
}
