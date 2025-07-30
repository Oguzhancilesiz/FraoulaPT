using FraoulaPT.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.PackageDTOs
{
    public class PackageUpdateDTO : PackageCreateDTO
    {
        public Guid Id { get; set; }
        public Status? Status { get; set; }
    }

}
