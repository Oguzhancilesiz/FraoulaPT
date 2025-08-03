using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FraoulaPT.Entity
{
    public class Product : IEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        public int StockQuantity { get; set; }
        
        [MaxLength(500)]
        public string ImageUrl { get; set; }
        
        public ProductCategory Category { get; set; }
        
        public bool IsFeatured { get; set; }
        
        // IEntity properties
        public Status Status { get; set; } = Status.Active;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public int AutoID { get; set; }
        
        // Influencer için özel alanlar
        public bool IsInfluencerChoice { get; set; }
        
        [MaxLength(500)]
        public string InfluencerComment { get; set; }
        
        // SEO ve rating
        [MaxLength(100)]
        public string Slug { get; set; }
        
        public decimal Rating { get; set; } = 5.0m;
        
        public int ReviewCount { get; set; } = 0;
    }

    public enum ProductCategory
    {
        Clothing = 1,        // Giyim
        Supplement = 2,      // Supplement
        Accessory = 3,       // Aksesuar
        Equipment = 4        // Ekipman
    }
}