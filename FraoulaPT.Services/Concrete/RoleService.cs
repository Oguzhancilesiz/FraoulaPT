using FraoulaPT.DTOs.RoleDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task AddRole(RoleAddDTO role)
        {
            try
            {
                AppRole appRole = new AppRole()
                {
                    Name = role.Name,
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    Status = Core.Enums.Status.Active
                };

                await _roleManager.CreateAsync(appRole);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteRole(Guid id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id.ToString());
                await _roleManager.DeleteAsync(role);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RoleDTO?> GetRole(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            RoleDTO dto = new RoleDTO();
            dto.Id = role.Id;
            dto.Name = role.Name;
            dto.CratedDate = role.CreatedDate;
            dto.Status = role.Status;


            return dto;
        }

        public async Task<List<RoleDTO>> GetRoles()
        {

            List<RoleDTO> roles = new List<RoleDTO>();
            if (_roleManager.Roles.Count() > 0)
            {
                roles = await _roleManager.Roles.Select(x => new RoleDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    CratedDate = x.CreatedDate,
                    Status = x.Status
                }).ToListAsync();
            }
            return roles;
        }
    }
}
