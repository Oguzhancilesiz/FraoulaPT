using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FraoulaPT.Entity
{
    public class ShippingRate : IEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string CompanyName { get; set; } // Yurtiçi Kargo, MNG, PTT vb.

        [Required]
        [MaxLength(100)]
        public string CityName { get; set; } // İstanbul, Ankara vb.

        [Column(TypeName = "decimal(18,2)")]
        public decimal BasePrice { get; set; } // Temel fiyat

        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerKg { get; set; } // Kg başına fiyat

        [Column(TypeName = "decimal(18,2)")]
        public decimal MaxWeight { get; set; } // Maksimum ağırlık (kg)

        public int EstimatedDays { get; set; } // Tahmini teslimat günü

        public bool IsActive { get; set; } = true;

        [MaxLength(500)]
        public string? Notes { get; set; }

        // IEntity properties
        public Status Status { get; set; } = Status.Active;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public int AutoID { get; set; }
    }
}