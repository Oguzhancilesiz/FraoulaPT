using FraoulaPT.DTOs.RoleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IRoleService
    {
        Task AddRole(RoleAddDTO role);
        Task DeleteRole(Guid id);
        Task<List<RoleDTO>> GetRoles();
        Task<RoleDTO?> GetRole(Guid id);

    }
}
