using FraoulaPT.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.ExtraPackageOptionDTOs
{
    public class ExtraPackageOptionUpdateDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public ExtraUsageType Type { get; set; }

        [Range(1, int.MaxValue)]
        public int Amount { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }
        public Status Status { get; set; }
    }
}
