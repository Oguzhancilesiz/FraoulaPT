using FraoulaPT.DTOs.AppRoleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IAppRoleService : IBaseService<
     AppRoleListDTO,
     AppRoleDetailDTO,
     AppRoleCreateDTO,
     AppRoleUpdateDTO>
    {

    }
}
