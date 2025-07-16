using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.UserDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager,IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<AppUser> GetByIdAsync(Guid id)
        {
            // Repository pattern kullanıyorsan:
            var user = await _unitOfWork.Repository<AppUser>().GetById(id);
            return user;
        }
        public async Task RegisterAsync(RegisterDTO dto)
        {
            if (dto.Password != dto.PasswordConfirm)
                throw new Exception("Şifreler eşleşmiyor!");

            var exist = await _userManager.FindByEmailAsync(dto.Email);
            if (exist != null) throw new Exception("Bu email ile kayıtlı kullanıcı mevcut.");

            var user = new AppUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                Status = Status.Active,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join(" | ", result.Errors.Select(x => x.Description)));

            // Kullanıcıya varsayılan "User" rolü ver (yoksa oluştur!)
            if (!await _roleManager.RoleExistsAsync("User"))
                await _roleManager.CreateAsync(new AppRole { Name = "User", Status = Status.Active, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now });

            await _userManager.AddToRoleAsync(user, "User");
            var userProfile = new UserProfile
            {
                AppUserId = user.Id,
                Status = Status.Active,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Gender = null,                   // Nullable olarak başlatılıyor
                BirthDate = null,                // Kullanıcıdan alınana kadar null
                HeightCm = null,                 // double? olarak nullable
                WeightKg = null,                 // double? olarak nullable
                BodyType = null,                 // Nullable enum
                BloodType = BloodType.None,                // Nullable enum
                PhoneNumber = string.Empty,
                Address = string.Empty,
                EmergencyContactName = string.Empty,
                EmergencyContactPhone = string.Empty,
                MedicalHistory = string.Empty,
                ChronicDiseases = string.Empty,
                CurrentMedications = string.Empty,
                Allergies = string.Empty,
                PastInjuries = string.Empty,
                CurrentPain = string.Empty,
                PregnancyStatus = null,          // Nullable bool
                LastCheckResults = string.Empty,
                SmokingAlcohol = string.Empty,
                Occupation = string.Empty,
                ExperienceLevel = ExperienceLevel.None,          // Nullable enum
                FavoriteSports = string.Empty,
                Notes = string.Empty,
                DietType = DietType.None,                 // Nullable enum
                CreatedByUserId = user.Id,
                UpdatedByUserId = user.Id,
                AutoID = 0
            };

            await _unitOfWork.Repository<UserProfile>().AddAsync(userProfile);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task LoginAsync(LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || user.Status != Status.Active)
                throw new Exception("Kullanıcı bulunamadı veya pasif!");

            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, dto.RememberMe, false);
            if (!result.Succeeded)
                throw new Exception("Giriş başarısız!");
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public List<UserDTO> GetAllUser()
        {
            throw new NotImplementedException();
        }

        public UserDTO GetByUser(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
