using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.PackageDTOs;
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
    public class PackageService : IPackageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PackageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Tüm package'leri getirir.
        /// </summary>
        /// <returns></returns>
        public async Task<List<PackageListDTO>> GetAllAsync()
        {
            var entities = await _unitOfWork.Repository<Package>()
                .Query()
                .OrderBy(x => x.Order)
                .ToListAsync();

            return entities.Adapt<List<PackageListDTO>>();
        }

        /// <summary>
        /// ID'sine göre bir package getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PackageDetailDTO> GetByIdAsync(Guid id)
        {
            var entity = await _unitOfWork.Repository<Package>().GetById(id);
            return entity?.Adapt<PackageDetailDTO>();
        }
        /// <summary>
        /// Yeni bir package ekler.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(PackageCreateDTO dto)
        {
            var entity = dto.Adapt<Package>();
            entity.Status = Status.Active;
            await _unitOfWork.Repository<Package>().AddAsync(entity);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
        /// <summary>
        /// Mevcut bir package'yi günceller.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(PackageUpdateDTO dto)
        {
            // Aynı ID'ye sahip entity context içinde zaten varsa onu çekiyoruz
            var existingEntity = await _unitOfWork.Repository<Package>().GetById(dto.Id);
            if (existingEntity == null)
                return false;

            // Sadece gelen dto'daki alanları mevcut entity'nin üzerine yazıyoruz
            dto.Adapt(existingEntity); // destination: existingEntity

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Belirtilen ID'ye sahip package'yi soft delete yapar.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> SoftDeleteAsync(Guid id)
        {
            var entity = await _unitOfWork.Repository<Package>().GetById(id);
            if (entity == null) return false;

            entity.Status = Status.Deleted;
            _unitOfWork.Repository<Package>().Update(entity);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }


}
