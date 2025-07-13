using FraoulaPT.Core.Abstracts;
using FraoulaPT.DTOs.UserDTOs;
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
    public class UserProfileService : IUserProfileService
    {
        private readonly IUnitOfWork _uow;

        public UserProfileService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ProfileViewDTO> GetProfileAsync(Guid appUserId)
        {
            var query = await _uow.Repository<UserProfile>().GetBy(x => x.AppUserId == appUserId && x.Status == FraoulaPT.Core.Enums.Status.Active);
            var entity = await query.Include(x => x.AppUser).FirstOrDefaultAsync();
            if (entity == null) return null;

            // Entity to DTO (manuel map, istersen AutoMapper ile de yapılabilir)
            return new ProfileViewDTO
            {
                FullName = entity.AppUser?.FullName,
                Email = entity.AppUser?.Email,
                Gender = entity.Gender,
                BirthDate = entity.BirthDate,
                HeightCm = entity.HeightCm,
                WeightKg = entity.WeightKg,
                BodyType = entity.BodyType,
                BloodType = entity.BloodType,
                PhoneNumber = entity.PhoneNumber,
                Address = entity.Address,
                EmergencyContactName = entity.EmergencyContactName,
                EmergencyContactPhone = entity.EmergencyContactPhone,
                MedicalHistory = entity.MedicalHistory,
                ChronicDiseases = entity.ChronicDiseases,
                CurrentMedications = entity.CurrentMedications,
                Allergies = entity.Allergies,
                PastInjuries = entity.PastInjuries,
                CurrentPain = entity.CurrentPain,
                PregnancyStatus = entity.PregnancyStatus,
                LastCheckResults = entity.LastCheckResults,
                SmokingAlcohol = entity.SmokingAlcohol,
                Occupation = entity.Occupation,
                ExperienceLevel = entity.ExperienceLevel,
                FavoriteSports = entity.FavoriteSports,
                Notes = entity.Notes,
                DietType = entity.DietType
            };
        }

        public async Task<ProfileEditDTO> GetProfileForEditAsync(Guid appUserId)
        {
            var query = await _uow.Repository<UserProfile>().GetBy(x => x.AppUserId == appUserId && x.Status == FraoulaPT.Core.Enums.Status.Active);
            var entity = await query.FirstOrDefaultAsync();
            if (entity == null) return null;

            return new ProfileEditDTO
            {
                Gender = entity.Gender,
                BirthDate = entity.BirthDate,
                HeightCm = entity.HeightCm,
                WeightKg = entity.WeightKg,
                BodyType = entity.BodyType,
                BloodType = entity.BloodType,
                PhoneNumber = entity.PhoneNumber,
                Address = entity.Address,
                EmergencyContactName = entity.EmergencyContactName,
                EmergencyContactPhone = entity.EmergencyContactPhone,
                MedicalHistory = entity.MedicalHistory,
                ChronicDiseases = entity.ChronicDiseases,
                CurrentMedications = entity.CurrentMedications,
                Allergies = entity.Allergies,
                PastInjuries = entity.PastInjuries,
                CurrentPain = entity.CurrentPain,
                PregnancyStatus = entity.PregnancyStatus,
                LastCheckResults = entity.LastCheckResults,
                SmokingAlcohol = entity.SmokingAlcohol,
                Occupation = entity.Occupation,
                ExperienceLevel = entity.ExperienceLevel,
                FavoriteSports = entity.FavoriteSports,
                Notes = entity.Notes,
                DietType = entity.DietType
            };
        }

        public async Task UpdateProfileAsync(Guid appUserId, ProfileEditDTO dto)
        {
            var query = await _uow.Repository<UserProfile>().GetBy(x => x.AppUserId == appUserId && x.Status == FraoulaPT.Core.Enums.Status.Active);
            var entity = await query.FirstOrDefaultAsync();

            if (entity == null)
                throw new Exception("Kullanıcı profili bulunamadı!");

            // Güncelleme (manuel map)
            entity.Gender = dto.Gender;
            entity.BirthDate = dto.BirthDate;
            entity.HeightCm = dto.HeightCm;
            entity.WeightKg = dto.WeightKg;
            entity.BodyType = dto.BodyType;
            entity.BloodType = dto.BloodType;
            entity.PhoneNumber = dto.PhoneNumber;
            entity.Address = dto.Address;
            entity.EmergencyContactName = dto.EmergencyContactName;
            entity.EmergencyContactPhone = dto.EmergencyContactPhone;
            entity.MedicalHistory = dto.MedicalHistory;
            entity.ChronicDiseases = dto.ChronicDiseases;
            entity.CurrentMedications = dto.CurrentMedications;
            entity.Allergies = dto.Allergies;
            entity.PastInjuries = dto.PastInjuries;
            entity.CurrentPain = dto.CurrentPain;
            entity.PregnancyStatus = dto.PregnancyStatus;
            entity.LastCheckResults = dto.LastCheckResults;
            entity.SmokingAlcohol = dto.SmokingAlcohol;
            entity.Occupation = dto.Occupation;
            entity.ExperienceLevel = dto.ExperienceLevel;
            entity.FavoriteSports = dto.FavoriteSports;
            entity.Notes = dto.Notes;
            entity.DietType = dto.DietType;

            await _uow.Repository<UserProfile>().Update(entity);
            await _uow.SaveChangesAsync();
        }
    }
}
