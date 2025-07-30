using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.ExtraRightDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class ExtraRightService : IExtraRightService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExtraRightService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ExtraRightListDTO>> GetAllAsync()
        {
            var query = await _unitOfWork.Repository<ExtraRight>()
                .Query()
                .Include(x => x.UserPackage)
                    .ThenInclude(up => up.AppUser)
                .Include(x => x.UserPackage.Package)
                .ToListAsync();

            return query.Select(x => new ExtraRightListDTO
            {
                Id = x.Id,
                RightType = x.RightType,
                Amount = x.Amount,
                PurchaseDate = x.PurchaseDate,
                PackageName = x.UserPackage.Package.Name,
                UserFullName = x.UserPackage.AppUser.FullName
            }).ToList();
        }

        public async Task<bool> AddAsync(ExtraRightCreateDTO dto)
        {
            var entity = dto.Adapt<ExtraRight>();
            entity.PurchaseDate = DateTime.UtcNow;

            await _unitOfWork.Repository<ExtraRight>().AddAsync(entity);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _unitOfWork.Repository<ExtraRight>().GetById(id);
            if (entity == null) return false;

            _unitOfWork.Repository<ExtraRight>().Delete(entity); // Status = Deleted
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddAsync(ExtraRightAddDTO dto)
        {
            // Aktif UserPackage bul
            var userPackage = await _unitOfWork.Repository<UserPackage>()
                .Query()
                .Where(up => up.AppUserId == dto.AppUserId && up.IsActive && up.Status == Status.Active)
                .OrderByDescending(up => up.CreatedDate)
                .FirstOrDefaultAsync();

            if (userPackage == null)
                return false;

            // Ek paketi oluştur
            var entity = new ExtraRight
            {
                Id = Guid.NewGuid(),
                UserPackageId = userPackage.Id,
                RightType = dto.RightType,
                Amount = dto.Amount,
                PurchaseDate = DateTime.UtcNow,
                PaymentId = "DEMO-PAYMENT-ID",
                Note = "Sistemden alındı",
                CreatedDate = DateTime.UtcNow,
                Status = Status.Active
            };

            // 🧠 İlgili UserPackage değerini güncelle
            if (dto.RightType == ExtraRightType.Question)
                userPackage.TotalQuestions = (userPackage.TotalQuestions ?? 0) + dto.Amount;
            else if (dto.RightType == ExtraRightType.Message)
                userPackage.TotalMessages = (userPackage.TotalMessages ?? 0) + dto.Amount;

            await _unitOfWork.Repository<ExtraRight>().AddAsync(entity);
            _unitOfWork.Repository<UserPackage>().Update(userPackage); // Güncellemeyi unutma

            return await _unitOfWork.SaveChangesAsync() > 0;
        }


    }
}
