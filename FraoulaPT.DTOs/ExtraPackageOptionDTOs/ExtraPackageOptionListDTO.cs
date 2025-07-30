using FraoulaPT.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.ExtraPackageOptionDTOs
{
    public class ExtraPackageOptionListDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ExtraUsageType Type { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}
