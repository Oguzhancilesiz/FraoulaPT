using FraoulaPT.Entity;
using System.ComponentModel.DataAnnotations;

namespace FraoulaPT.DTOs.ProductDTOs
{
    public class ProductCreateDTO
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stok miktarı 0 veya daha büyük olmalıdır")]
        public int StockQuantity { get; set; }

        [MaxLength(500)]
        public string ImageUrl { get; set; }

        [Required]
        public ProductCategory Category { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsInfluencerChoice { get; set; }

        [MaxLength(500)]
        public string? InfluencerComment { get; set; }

        [MaxLength(100)]
        public string Slug { get; set; }

        [Range(1, 5, ErrorMessage = "Rating 1-5 arasında olmalıdır")]
        public decimal Rating { get; set; } = 5.0m;

        public int ReviewCount { get; set; } = 0;
    }
}