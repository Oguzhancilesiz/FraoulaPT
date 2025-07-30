using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
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

        public async Task<bool> HasActivePackageAsync(Guid userId)
        {
            return await _unitOfWork.Repository<UserPackage>()
                .Query()
                .AnyAsync(x => x.AppUserId == userId && x.Status == Status.Active && x.IsActive && x.EndDate > DateTime.UtcNow);
        }

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

    }

}
