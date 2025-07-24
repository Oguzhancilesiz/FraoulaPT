using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IBaseService<TListDto, TDetailDto, TCreateDto, TUpdateDto>
       where TListDto : class
       where TDetailDto : class
       where TCreateDto : class
       where TUpdateDto : class
    {
        Task<List<TListDto>> GetAllAsync();
        Task<TDetailDto> GetByIdAsync(Guid id);
        Task<Guid> AddAsync(TCreateDto dto);
        Task<bool> UpdateAsync(TUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> SoftDeleteAsync(Guid id);
        Task<int> CountAsync();
        Task<bool> AnyAsync(Guid id);
    }
}
