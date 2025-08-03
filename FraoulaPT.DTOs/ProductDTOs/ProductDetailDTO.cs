using FraoulaPT.Entity;

namespace FraoulaPT.DTOs.ProductDTOs
{
    public class ProductDetailDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string ImageUrl { get; set; }
        public ProductCategory Category { get; set; }
        public string CategoryName { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsInfluencerChoice { get; set; }
        public string? InfluencerComment { get; set; }
        public string Slug { get; set; }
        public decimal Rating { get; set; }
        public int ReviewCount { get; set; }
    }
}