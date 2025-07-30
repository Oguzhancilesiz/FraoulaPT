using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.UserWeeklyFormDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.Services.MapsterMap;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class UserWeeklyFormService :
       BaseService<UserWeeklyForm, UserWeeklyFormListDTO, UserWeeklyFormDetailDTO, UserWeeklyFormCreateDTO, UserWeeklyFormUpdateDTO>,
       IUserWeeklyFormService
    {
        private readonly IBaseRepository<Media> _mediaRepo;

        public UserWeeklyFormService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _mediaRepo = unitOfWork.Repository<Media>();
        }
        public async Task<List<UserWeeklyFormAdminListDTO>> GetAllForAdminAsync()
        {
            var query = await _repository.GetBy(x => x.Status != Core.Enums.Status.Deleted);
            var list = await query
                .Include(x => x.AppUser)
                .Include(x => x.ProgressPhotos)
                .OrderByDescending(x => x.FormDate)
                .ToListAsync();

            var result = list.Select(x => new UserWeeklyFormAdminListDTO
            {
                Id = x.Id,
                AppUserId = x.AppUserId,
                FormDate = x.FormDate,
                Weight = x.Weight,
                FatPercent = x.FatPercent,
                MuscleMass = x.MuscleMass,
                CoachFeedback = x.CoachFeedback,
                UserFullName = x.AppUser?.FullName,
                UserEmail = x.AppUser?.Email,
                Status = x.Status,
                ProgressPhotoUrls = x.ProgressPhotos?
                    .Where(p => p.Status == Core.Enums.Status.Active)
                    .Select(p => p.Url)
                    .ToList() ?? new List<string>()
            }).ToList();

            return result;
        }


        public async Task<List<UserWeeklyFormListDTO>> GetListByUserAsync(Guid userId)
        {
            var query = await _repository.GetBy(x => x.AppUserId == userId);
            var list = await query
                .Include(x => x.ProgressPhotos)
                .Include(x => x.WorkoutProgram)
                .Include(x => x.AppUser)
                .ToListAsync();

            var result = list
                    .DistinctBy(x => x.Id) // id ye göre eşsizleştir
                .Select(x => new UserWeeklyFormListDTO
            {
                Id = x.Id,
                UserId=x.AppUserId,
                FormDate = x.FormDate,
                Weight = x.Weight,
                FatPercent = x.FatPercent,
                MuscleMass = x.MuscleMass,
                Waist = x.Waist,
                Hip = x.Hip,
                Chest = x.Chest,
                CoachFeedback = x.CoachFeedback ?? "",
                Status= x.Status,
                ProgressPhotoUrls = x.ProgressPhotos != null
                    ? x.ProgressPhotos
                        .Where(p => p.Status == Core.Enums.Status.Active)
                        .Select(p => p.Url)
                        .ToList()
                    : new List<string>(),
                HasWorkoutProgram = x.WorkoutProgram != null,
                WorkoutProgramId = x.WorkoutProgram?.Id
            }).ToList();

            return result;
        }

        public async Task<Guid> AddWithFilesAsync(UserWeeklyFormCreateDTO dto, Guid userId, List<IFormFile> files, string rootPath)
        {
            var entity = dto.Adapt<UserWeeklyForm>();
            entity.AppUserId = userId;
            entity.Status = Core.Enums.Status.Pending;
            entity.CoachFeedback = "En kısa sürede dönüş sağlanacaktır...";
            entity.CreatedByUserId = userId;
            entity.UpdatedByUserId = userId;
            entity.CreatedDate = DateTime.UtcNow;
            entity.ModifiedDate = DateTime.UtcNow;

            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            await SaveMediaFilesAsync(files, entity.Id, rootPath);
            return entity.Id;
        }
        public async Task<UserWeeklyFormListDTO> GetLastFormByUserIdAsync(Guid userId)
        {
            var query = await _repository.GetBy(x => x.AppUserId == userId);
            var lastForm = await query
                .OrderByDescending(x => x.FormDate)
                .FirstOrDefaultAsync();

            return lastForm?.Adapt<UserWeeklyFormListDTO>();
        }

        public async Task<bool> UpdateWithFilesAsync(UserWeeklyFormUpdateDTO dto, List<IFormFile> newFiles, string rootPath)
        {
            var entity = await _repository.GetById(dto.Id, include: q => q.Include(x => x.ProgressPhotos));
            if (entity == null) return false;

            dto.Adapt(entity);

            // Silinecek medya
            if (dto.DeletedMediaIds is { Count: > 0 })
            {
                foreach (var mediaId in dto.DeletedMediaIds)
                {
                    var media = await _mediaRepo.GetById(mediaId);
                    if (media?.UserWeeklyFormId == entity.Id)
                    {
                        media.Status = Core.Enums.Status.Deleted;
                        await _mediaRepo.Update(media);
                    }
                }
            }

            await SaveMediaFilesAsync(newFiles, entity.Id, rootPath);

            await _repository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        private async Task SaveMediaFilesAsync(List<IFormFile> files, Guid formId, string rootPath)
        {
            if (files is not { Count: > 0 }) return;

            var folder = Path.Combine(rootPath, "uploads", "weeklyforms");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            foreach (var file in files)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var fullPath = Path.Combine(folder, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                await file.CopyToAsync(stream);

                var media = new Media
                {
                    Url = "/uploads/weeklyforms/" + fileName,
                    MediaType = Core.Enums.MediaType.Image,
                    AltText = "Progress Photo",
                    UserWeeklyFormId = formId,
                    CreatedByUserId = formId,
                    UpdatedByUserId = formId,
                    Status = Core.Enums.Status.Active,
                    ThumbnailUrl = "/uploads/weeklyforms/" + fileName
                };

                await _mediaRepo.AddAsync(media);
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<UserWeeklyFormDetailDTO> GetDetailWithPhotosByIdAsync(Guid id)
        {
            var query = await _repository.GetBy(x => x.Id == id);
            var entity = await query
                .Include(x => x.ProgressPhotos)
                .Include(x => x.AppUser)
                .FirstOrDefaultAsync();

            if (entity == null)
                return null;

            var dto = new UserWeeklyFormDetailDTO
            {
                Id = entity.Id,
                FormDate = entity.FormDate,
                Weight = entity.Weight,
                FatPercent = entity.FatPercent,
                MuscleMass = entity.MuscleMass,
                Waist = entity.Waist,
                Hip = entity.Hip,
                Chest = entity.Chest,
                Arm = entity.Arm,
                Leg = entity.Leg,
                RestingPulse = entity.RestingPulse,
                BloodPressure = entity.BloodPressure,
                Vo2Max = entity.Vo2Max,
                FlexibilityNotes = entity.FlexibilityNotes,
                UserNote = entity.UserNote,
                CoachFeedback = entity.CoachFeedback,
                Status = entity.Status,
                ProgressPhotoUrls = entity.ProgressPhotos?
                    .Where(p => p.Status == Core.Enums.Status.Active)
                    .Select(p => p.Url)
                    .ToList()
            };

            return dto;
        }
        public async Task<UserLastFormDTO> GetLastFormByUserAsync(Guid userId)
        {
            var form = await _unitOfWork.Repository<UserWeeklyForm>()
                .Query()
                .Where(f => f.AppUserId == userId && f.Status != Status.Deleted)
                .OrderByDescending(f => f.FormDate)
                .Include(f => f.ProgressPhotos)
                .FirstOrDefaultAsync();

            if (form == null) return null;

            var dto = new UserLastFormDTO
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
                RestingPulse = form.RestingPulse,
                BloodPressure = form.BloodPressure,
                Vo2Max = form.Vo2Max,
                FlexibilityNotes = form.FlexibilityNotes,
                UserNote = form.UserNote,
                CoachFeedback = form.CoachFeedback,
                ProgressPhotoUrls = form.ProgressPhotos
                    .Where(p => p.Status != Status.Deleted)
                    .Select(p => p.Url)
                    .ToList()
            };

            return dto;
        }

    }
}
