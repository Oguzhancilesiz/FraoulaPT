using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.PackageDTO;
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
    public class PackageService : IPackageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PackageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<PackageListDTO>> GetActivePackagesAsync()
        {
            // 1. Sadece aktif olan paketleri al
            var query = await _unitOfWork.Repository<Package>().GetBy(x => x.IsActive);

            // 2. Sırala (Order)
            var packages = await query.OrderBy(x => x.Order).ToListAsync();

            // 3. Manuel map
            var result = packages.Select(x => new PackageListDTO
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                PackageType = x.PackageType,
                SubscriptionPeriod = x.SubscriptionPeriod,
                MaxQuestionsPerPeriod = x.MaxQuestionsPerPeriod,
                MaxMessagesPerPeriod = x.MaxMessagesPerPeriod,
                Price = x.Price,
                ImageUrl = x.ImageUrl,
                HighlightColor = x.HighlightColor,
                Order = x.Order,
                Features = x.Features
            }).ToList();

            return result;
        }

        public async Task<PackageListDTO> GetByIdAsync(Guid id)
        {
            var package = await _unitOfWork.Repository<Package>().GetById(id);
            if (package == null) return null;

            // Manuel DTO map
            var dto = new PackageListDTO
            {
                Id = package.Id,
                Name = package.Name,
                Description = package.Description,
                PackageType = package.PackageType,
                SubscriptionPeriod = package.SubscriptionPeriod,
                MaxQuestionsPerPeriod = package.MaxQuestionsPerPeriod,
                MaxMessagesPerPeriod = package.MaxMessagesPerPeriod,
                Price = package.Price,
                ImageUrl = package.ImageUrl,
                HighlightColor = package.HighlightColor,
                Order = package.Order,
                Features = package.Features
            };
            return dto;
        }


        public async Task AddAsync(PackageCreateDTO dto)
        {
            // Manuel entity oluşturma
            var package = new Package
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                PackageType = dto.PackageType,
                SubscriptionPeriod = dto.SubscriptionPeriod,
                MaxQuestionsPerPeriod = dto.MaxQuestionsPerPeriod,
                MaxMessagesPerPeriod = dto.MaxMessagesPerPeriod,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                HighlightColor = dto.HighlightColor,
                Order = dto.Order,
                Features = dto.Features,
                IsActive = dto.IsActive,
                Status = Status.Active, // Eğer default aktifse
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
            };

            await _unitOfWork.Repository<Package>().AddAsync(package);
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task UpdateAsync(PackageEditDTO dto)
        {
            var entity = await _unitOfWork.Repository<Package>().GetById(dto.Id);
            if (entity == null) throw new Exception("Paket bulunamadı.");

            // Manuel güncelleme
            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.PackageType = dto.PackageType;
            entity.SubscriptionPeriod = dto.SubscriptionPeriod;
            entity.MaxQuestionsPerPeriod = dto.MaxQuestionsPerPeriod;
            entity.MaxMessagesPerPeriod = dto.MaxMessagesPerPeriod;
            entity.Price = dto.Price;
            entity.ImageUrl = dto.ImageUrl;
            entity.HighlightColor = dto.HighlightColor;
            entity.Order = dto.Order;
            entity.Features = dto.Features;
            entity.IsActive = dto.IsActive;
            entity.ModifiedDate = DateTime.UtcNow;

            await _unitOfWork.Repository<Package>().Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task DeleteAsync(Guid id)
        {
            var entity = await _unitOfWork.Repository<Package>().GetById(id);
            if (entity == null) throw new Exception("Paket bulunamadı.");

            await _unitOfWork.Repository<Package>().Delete(entity);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
