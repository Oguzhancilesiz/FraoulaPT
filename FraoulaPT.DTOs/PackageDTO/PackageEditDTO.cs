using FraoulaPT.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.PackageDTO
{
    public class PackageEditDTO : PackageCreateDTO
    {
        public Guid Id { get; set; }
    }
}
