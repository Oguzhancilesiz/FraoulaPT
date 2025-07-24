using FraoulaPT.Core.Abstracts;
using FraoulaPT.DTOs.UserPackageDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class UserPackageService : BaseService<
    UserPackage,
    UserPackageListDTO,
    UserPackageDetailDTO,
    UserPackageCreateDTO,
    UserPackageUpdateDTO>, IUserPackageService
    {
        public UserPackageService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

}
