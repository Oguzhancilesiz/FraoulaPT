using FraoulaPT.Core.Abstracts;
using FraoulaPT.DTOs.MediaDTOs;
using FraoulaPT.DTOs.UserWeeklyFormDTO;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class UserWeeklyFormService : IUserWeeklyFormService
    {
        private readonly IUnitOfWork _uow;

        public UserWeeklyFormService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // Kullanıcı yeni form ekler (progress photo dosya yüklemesini dışarıdan Media olarak alıyoruz)
        public async Task AddFormAsync(UserWeeklyFormCreateDTO dto, List<Media> progressPhotos)
        {
            

            var form = new UserWeeklyForm
            {
                UserPackageId = dto.UserPackageId,
                FormDate = dto.FormDate,
                Weight = dto.Weight,
                FatPercent = dto.FatPercent,
                MuscleMass = dto.MuscleMass,
                Waist = dto.Waist,
                Hip = dto.Hip,
                Chest = dto.Chest,
                Arm = dto.Arm,
                Leg = dto.Leg,
                RestingPulse = dto.RestingPulse,
                BloodPressure = dto.BloodPressure,
                Vo2Max = dto.Vo2Max,
                FlexibilityNotes = dto.FlexibilityNotes,
                UserNote = dto.UserNote,
                Status = FraoulaPT.Core.Enums.Status.Pending,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                CoachFeedback = "Henüz Dönüş Sağlanmadı...",
                ProgressPhotos = progressPhotos
            };

            await _uow.Repository<UserWeeklyForm>().AddAsync(form);
            await _uow.SaveChangesAsync();
        }

        // Kullanıcıya ait formları getir (en yeni önce)
        public async Task<List<UserWeeklyFormDTO>> GetUserFormsAsync(Guid userPackageId)
        {
            var query = await _uow.Repository<UserWeeklyForm>()
                .GetBy(x => x.UserPackageId == userPackageId);

            var forms = await query
                .Include(x => x.ProgressPhotos)
                .OrderByDescending(x => x.AutoID)
                .ToListAsync();

            return forms.Select(form => new UserWeeklyFormDTO
            {
                Id = form.Id,
                FormDate = form.FormDate,
                Weight = form.Weight,
                FatPercent = form.FatPercent,
                MuscleMass = form.MuscleMass,
                Waist = form.Waist,
                Hip = form.Hip,
                Chest = form.Chest,
                Arm = form.Arm,
                Leg = form.Leg,
                FlexibilityNotes = form.FlexibilityNotes,
                UserNote = form.UserNote,
                CoachFeedback = form.CoachFeedback,
                Status = form.Status,
                ProgressPhotos = form.ProgressPhotos?.Select(m => new MediaDTO
                {
                    Id = m.Id,
                    Url = m.Url,
                    AltText = m.AltText,
                    MediaType = m.MediaType,
                    ThumbnailUrl = m.ThumbnailUrl
                }).ToList()
            }).ToList();
        }

        // Hoca feedback ekler
        public async Task AddCoachFeedbackAsync(UserWeeklyFormCoachFeedbackDTO dto)
        {
            var query = await _uow.Repository<UserWeeklyForm>().GetBy(x => x.Id == dto.FormId);
            var form = await query.FirstOrDefaultAsync();
            if (form == null) throw new Exception("Form bulunamadı!");

            form.CoachFeedback = dto.CoachFeedback;
            form.Status = FraoulaPT.Core.Enums.Status.Read;
            form.ModifiedDate = DateTime.UtcNow;

            await _uow.Repository<UserWeeklyForm>().Update(form);
            await _uow.SaveChangesAsync();
        }
    }
}
