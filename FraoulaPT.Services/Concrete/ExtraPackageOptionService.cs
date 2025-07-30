using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.ExtraPackageOptionDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FraoulaPT.Services.Concrete
{
    public class ExtraPackageOptionService : IExtraPackageOptionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExtraPackageOptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<ExtraPackageOptionListDTO>> GetPublicListAsync()
        {
            var repo = _unitOfWork.Repository<ExtraPackageOption>();
            var list = await repo.Query()
                                 .Where(x => x.Status != Status.Deleted && x.IsActive)
                                 .ToListAsync();
            return list.Adapt<List<ExtraPackageOptionListDTO>>();
        }

        public async Task<List<ExtraPackageOptionListDTO>> GetAllAsync()
        {
            var repo = _unitOfWork.Repository<ExtraPackageOption>();
            var list = await repo.GetAll();
            return list.Adapt<List<ExtraPackageOptionListDTO>>();
        }

        public async Task<ExtraPackageOptionUpdateDTO?> GetByIdAsync(Guid id)
        {
            var entity = await _unitOfWork.Repository<ExtraPackageOption>().GetById(id);
            if (entity == null || entity.Status == Status.Deleted)
                return null;

            return entity.Adapt<ExtraPackageOptionUpdateDTO>();
        }

        public async Task<bool> CreateAsync(ExtraPackageOptionCreateDTO dto)
        {
            var entity = dto.Adapt<ExtraPackageOption>();
            await _unitOfWork.Repository<ExtraPackageOption>().AddAsync(entity);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(ExtraPackageOptionUpdateDTO dto)
        {
            var repo = _unitOfWork.Repository<ExtraPackageOption>();
            var existing = await repo.GetById(dto.Id);
            if (existing == null || existing.Status == Status.Deleted)
                return false;

            dto.Adapt(existing);
            repo.Update(existing);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> SoftDeleteAsync(Guid id)
        {
            var repo = _unitOfWork.Repository<ExtraPackageOption>();
            var entity = await repo.GetById(id);
            if (entity == null || entity.Status == Status.Deleted)
                return false;

            entity.Status = Status.Deleted;
            entity.IsActive = false;
            repo.Update(entity);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> ToggleActiveAsync(Guid id)
        {
            var repo = _unitOfWork.Repository<ExtraPackageOption>();
            var entity = await repo.GetById(id);
            if (entity == null || entity.Status == Status.Deleted)
                return false;

            entity.IsActive = !entity.IsActive;
            repo.Update(entity);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
