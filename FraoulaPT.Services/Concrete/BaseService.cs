using FraoulaPT.Core.Abstracts;
using FraoulaPT.Services.Abstracts;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public abstract class BaseService<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto> : IBaseService<TListDto, TDetailDto, TCreateDto, TUpdateDto>
        where TEntity : class, IEntity, new()
        where TListDto : class
        where TDetailDto : class
        where TCreateDto : class
        where TUpdateDto : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IBaseRepository<TEntity> _repository;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.Repository<TEntity>();
        }

        public virtual async Task<List<TListDto>> GetAllAsync()
        {
            var entities = await _repository.GetAll();
            return entities.Adapt<List<TListDto>>();
        }

        public virtual async Task<TDetailDto> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetById(id);
            return entity.Adapt<TDetailDto>();
        }

        public virtual async Task<Guid> AddAsync(TCreateDto dto)
        {
            var entity = dto.Adapt<TEntity>();
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity.Id;
        }

        public virtual async Task<bool> UpdateAsync(TUpdateDto dto)
        {
            var entity = dto.Adapt<TEntity>();
            if (entity.CreatedDate == default || entity.CreatedDate < new DateTime(1753, 1, 1))
                entity.CreatedDate = DateTime.UtcNow;
            if (entity.ModifiedDate == null || entity.ModifiedDate < new DateTime(1753, 1, 1))
                entity.ModifiedDate = DateTime.UtcNow;
            _repository.Update(entity);


            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null)
                return false;

            await _repository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public virtual async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        public virtual async Task<bool> AnyAsync(Guid id)
        {
            return await _repository.AnyAsync(x => x.Id == id);
        }

        public virtual async Task<bool> SoftDeleteAsync(Guid id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null)
                return false;

            // Status'u doğrudan enum Deleted yaptık
            entity.GetType().GetProperty("Status")?.SetValue(entity, FraoulaPT.Core.Enums.Status.Deleted);

            _repository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
