using FraoulaPT.DTOs.PackageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IPackageService : IBaseService<
      PackageListDTO,
      PackageDetailDTO,
      PackageCreateDTO,
      PackageUpdateDTO>
    { }
}
