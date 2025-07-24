using FraoulaPT.Core.Abstracts;
using FraoulaPT.DTOs.PackageDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class PackageService : BaseService<
    Package,
    PackageListDTO,
    PackageDetailDTO,
    PackageCreateDTO,
    PackageUpdateDTO>, IPackageService
    {
        public PackageService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

}
