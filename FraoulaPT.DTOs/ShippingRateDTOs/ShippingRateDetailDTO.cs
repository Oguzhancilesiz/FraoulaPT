namespace FraoulaPT.DTOs.ShippingRateDTOs
{
    public class ShippingRateDetailDTO
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string CityName { get; set; }
        public decimal BasePrice { get; set; }
        public decimal PricePerKg { get; set; }
        public decimal MaxWeight { get; set; }
        public int EstimatedDays { get; set; }
        public bool IsActive { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}