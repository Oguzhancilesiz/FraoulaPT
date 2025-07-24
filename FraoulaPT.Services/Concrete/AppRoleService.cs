using FraoulaPT.Core.Abstracts;
using FraoulaPT.DTOs.AppRoleDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class AppRoleService : BaseService<
    AppRole,
    AppRoleListDTO,
    AppRoleDetailDTO,
    AppRoleCreateDTO,
    AppRoleUpdateDTO>, IAppRoleService
    {
        public AppRoleService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

}
