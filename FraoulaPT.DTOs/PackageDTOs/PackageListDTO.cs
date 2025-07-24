using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.PackageDTOs
{
    public class PackageListDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public FraoulaPT.Core.Enums.PackageType PackageType { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }

}
