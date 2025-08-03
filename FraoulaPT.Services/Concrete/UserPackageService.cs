using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.DashboardDTOs;
using FraoulaPT.DTOs.ExtraRightDTOs;
using FraoulaPT.DTOs.UserPackageDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class UserPackageService : IUserPackageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        public UserPackageService(IUnitOfWork unitOfWork,UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        /// <summary>
        /// Yeni bir kullanıcı paketi oluşturur.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(UserPackageCreateDTO dto)
        {
            // Kullanıcı kontrolü
            var user = await _userManager.FindByIdAsync(dto.AppUserId.ToString());
            if (user == null) return false;

            // Paket kontrolü
            var package = await _unitOfWork.Repository<Package>().GetById(dto.PackageId);
            if (package == null) return false;

            var now = DateTime.UtcNow;

            // DTO'dan UserPackage'e map'le
            var entity = dto.Adapt<UserPackage>();

            // Zorunlu alanlar
            entity.Id = Guid.NewGuid();
            entity.AppUserId = dto.AppUserId; // direkt dto'dan al
            entity.PackageId = dto.PackageId;

            // Tarihler
            entity.StartDate = now;
            entity.EndDate = package.SubscriptionPeriod switch
            {
                SubscriptionPeriod.Daily => now.AddDays(1),
                SubscriptionPeriod.Weekly => now.AddDays(7),
                SubscriptionPeriod.Monthly => now.AddMonths(1),
                SubscriptionPeriod.Yearly => now.AddYears(1),
                SubscriptionPeriod.Lifetime => now.AddYears(100),
                SubscriptionPeriod.Trial => now.AddDays(3),
                SubscriptionPeriod.Custom => now.AddDays(15),
                _ => now.AddMonths(1)
            };

            // Diğer alanlar
            entity.CreatedDate = now;
            entity.ModifiedDate = now;
            entity.CreatedByUserId = dto.AppUserId;
            entity.UpdatedByUserId = dto.AppUserId;

            entity.IsActive = true;
            entity.Status = Status.Active;
            entity.UsedMessages = 0;
            entity.UsedQuestions = 0;
            entity.RenewalCount = 0;
            entity.IsRenewable = false;
            entity.PaymentId = "DEMO-PAYMENT-ID-12345";
            entity.LastPaymentDate = now;
            entity.CancelReason = "PAKET DEVAM EDİYOR";
            entity.TotalQuestions = package.MaxQuestionsPerPeriod;
            entity.TotalMessages = package.MaxMessagesPerPeriod;
            await _unitOfWork.Repository<UserPackage>().AddAsync(entity);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
        /// <summary>
        /// Kullanıcıya ait aktif bir paketin olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> HasActivePackageAsync(Guid userId)
        {
            return await _unitOfWork.Repository<UserPackage>()
                .Query()
                .AnyAsync(x => x.AppUserId == userId && x.Status == Status.Active && x.IsActive && x.EndDate > DateTime.UtcNow);
        }
        /// <summary>
        /// Kullanıcıya ait tüm paketleri getirir.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<UserPackageDetailDTO>> GetPackagesByUserAsync(Guid userId)
        {
            var packages = await _unitOfWork.Repository<UserPackage>()
                .Query()
                .Where(x => x.AppUserId == userId && x.Status != Status.Deleted)
                .Include(x => x.Package)
                .Include(x => x.ExtraRights)
                .OrderByDescending(x => x.StartDate)
                .ToListAsync();

            return packages.Select(p => new UserPackageDetailDTO
            {
                Id = p.Id,
                PackageName = p.Package.Name,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                IsActive = p.IsActive,
                UsedMessages = p.UsedMessages,
                UsedQuestions = p.UsedQuestions,
                TotalMessages = p.TotalMessages ?? 0,
                TotalQuestions = p.TotalQuestions ?? 0,
                ExtraRights = p.ExtraRights
                                .Where(er => er.Status != Status.Deleted)
                                .Select(er => new ExtraRightDTO
                                {
                                    RightType = er.RightType,
                                    Amount = er.Amount,
                                    PurchaseDate = er.PurchaseDate,
                                    Note = er.Note
                                }).ToList()
            }).ToList();
        }
        /// <summary>
        /// Kullanıcıya ait aktif paketin durumunu getirir.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserPackageStatusDTO?> GetActivePackageStatusAsync(Guid userId)
        {
            var package = await _unitOfWork.Repository<UserPackage>()
                .Query()
                .Include(x => x.ExtraRights)
                .Include(x => x.Package)
                .Where(x => x.AppUserId == userId && x.Status == Status.Active && x.IsActive)
                .OrderByDescending(x => x.StartDate)
                .FirstOrDefaultAsync();

            if (package == null)
                return null;

            return new UserPackageStatusDTO
            {
                UserPackageId = package.Id,
                StartDate = package.StartDate,
                EndDate = package.EndDate,
                UsedMessages = package.UsedMessages,
                UsedQuestions = package.UsedQuestions,
                TotalMessages = package.TotalMessages ?? 0,
                TotalQuestions = package.TotalQuestions ?? 0,
                ExtraMessageRights = package.ExtraRights.Where(x => x.RightType == ExtraRightType.Message && x.Status == Status.Active).Sum(x => x.Amount),
                ExtraQuestionRights = package.ExtraRights.Where(x => x.RightType == ExtraRightType.Question && x.Status == Status.Active).Sum(x => x.Amount)
            };
        }
        /// <summary>
        /// Kullanıcının mesaj kullanımını artırır. Eğer aktif paketi yoksa veya süresi dolmuşsa false döner.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> IncrementUsedMessageAsync(Guid userId)
        {
            var package = await _unitOfWork.Repository<UserPackage>()
                .Query()
                .Where(x => x.AppUserId == userId && x.Status == Status.Active && x.IsActive)
                .OrderByDescending(x => x.StartDate)
                .FirstOrDefaultAsync();

            if (package == null || package.EndDate <= DateTime.UtcNow)
                return false;

            package.UsedMessages += 1;
            package.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.Repository<UserPackage>().Update(package);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
        /// <summary>
        /// Kullanıcının soru kullanımını artırır. Eğer aktif paketi yoksa veya süresi dolmuşsa false döner.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> IncrementUsedQuestionAsync(Guid userId)
        {
            var package = await _unitOfWork.Repository<UserPackage>()
                .Query()
                .Where(x => x.AppUserId == userId && x.Status == Status.Active && x.IsActive)
                .OrderByDescending(x => x.StartDate)
                .FirstOrDefaultAsync();

            if (package == null || package.EndDate <= DateTime.UtcNow)
                return false;

            package.UsedQuestions += 1;
            package.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.Repository<UserPackage>().Update(package);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
        /// <summary>
        /// Kullanıcının kalan gün sayısını getirir. Eğer aktif paketi yoksa veya süresi dolmuşsa null döner.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int?> GetRemainingDaysAsync(Guid userId)
        {
            var package = await _unitOfWork.Repository<UserPackage>()
                .Query()
                .Where(x => x.AppUserId == userId && x.Status == Status.Active && x.IsActive && x.EndDate > DateTime.UtcNow)
                .OrderByDescending(x => x.StartDate)
                .FirstOrDefaultAsync();

            if (package == null)
                return null;

            return (package.EndDate - DateTime.UtcNow).Days;
        }


        //dashboard icin
        public async Task<int> GetActivePackageCountAsync()
        {
            return await _unitOfWork.Repository<UserPackage>()
                .Query()
                .CountAsync(p => p.Status == Status.Active && p.EndDate > DateTime.UtcNow);
        }

        public async Task<decimal> GetMonthlyRevenueAsync()
        {
            var startOfMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            return await _unitOfWork.Repository<UserPackage>()
                .Query()
                .Where(p => p.StartDate >= startOfMonth && p.Status == Status.Active)
                .SumAsync(p => p.AutoID);
        }

        public async Task<int> GetExpiringPackagesCountAsync()
        {
            var thresholdDate = DateTime.UtcNow.AddDays(7);
            return await _unitOfWork.Repository<UserPackage>()
                .Query()
                .CountAsync(p => p.Status == Status.Active && p.EndDate <= thresholdDate);
        }

        public async Task<List<ExpiringPackageDTO>> GetExpiringPackagesAsync(int limit)
        {
            var thresholdDate = DateTime.UtcNow.AddDays(7);
            return await _unitOfWork.Repository<UserPackage>()
                .Query()
                .Where(p => p.Status == Status.Active && p.EndDate <= thresholdDate)
                .OrderBy(p => p.EndDate)
                .Select(p => new ExpiringPackageDTO
                {
                    UserName = p.AppUser.FullName,
                    DaysLeft = (p.EndDate - DateTime.UtcNow).Days
                })
                .Take(limit)
                .ToListAsync();
        }

        public async Task<List<LowQuotaUserDTO>> GetLowMessageQuotaUsersAsync(int limit)
        {
            return await _unitOfWork.Repository<UserPackage>()
                .Query()
                .Where(p => p.Status == Status.Active && (p.TotalMessages - p.UsedMessages) <= 3)
                .Select(p => new LowQuotaUserDTO
                {
                    UserName = p.AppUser.FullName,
                    RemainingMessages = p.ChatMessages.Count(m => m.Status == Status.Active && m.ModifiedDate > DateTime.UtcNow)
                })
                .OrderBy(p => p.RemainingMessages)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<int> GetUnapprovedPaymentsCountAsync()
        {
            return await _unitOfWork.Repository<UserPackage>()
                .Query()
                .CountAsync(p => p.Status==Status.Approved); // Onay bekleyen
        }

    }

}
