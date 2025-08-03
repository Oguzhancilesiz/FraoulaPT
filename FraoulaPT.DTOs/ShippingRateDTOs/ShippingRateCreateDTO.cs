using System.ComponentModel.DataAnnotations;

namespace FraoulaPT.DTOs.ShippingRateDTOs
{
    public class ShippingRateCreateDTO
    {
        [Required]
        [MaxLength(100)]
        public string CompanyName { get; set; }

        [Required]
        [MaxLength(100)]
        public string CityName { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Temel fiyat 0'dan büyük olmalıdır")]
        public decimal BasePrice { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Kg başına fiyat 0 veya daha büyük olmalıdır")]
        public decimal PricePerKg { get; set; }

        [Required]
        [Range(0.1, 1000, ErrorMessage = "Maksimum ağırlık 0.1-1000 kg arasında olmalıdır")]
        public decimal MaxWeight { get; set; }

        [Required]
        [Range(1, 30, ErrorMessage = "Teslimat süresi 1-30 gün arasında olmalıdır")]
        public int EstimatedDays { get; set; }

        public bool IsActive { get; set; } = true;

        [MaxLength(500)]
        public string? Notes { get; set; }
    }
}