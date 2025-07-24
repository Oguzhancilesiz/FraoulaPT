using FraoulaPT.DTOs.UserPackageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{

    public interface IUserPackageService : IBaseService<
        UserPackageListDTO,
        UserPackageDetailDTO,
        UserPackageCreateDTO,
        UserPackageUpdateDTO>
    { }
}
