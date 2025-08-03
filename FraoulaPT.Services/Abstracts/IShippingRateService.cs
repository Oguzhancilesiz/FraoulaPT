using FraoulaPT.Core.Abstracts;
using FraoulaPT.DTOs.ShippingRateDTOs;
using FraoulaPT.Entity;

namespace FraoulaPT.Services.Abstracts
{
    public interface IShippingRateService : IBaseService<ShippingRateDetailDTO, ShippingRateDetailDTO, ShippingRateCreateDTO, ShippingRateUpdateDTO>
    {
        Task<List<ShippingRate>> GetByCityAsync(string cityName);
        Task<List<ShippingRate>> GetByCompanyAsync(string companyName);
        Task<List<ShippingRate>> GetActiveRatesAsync();
        Task<decimal> CalculateShippingFeeAsync(string cityName, string companyName, decimal weight);
        Task<List<ShippingRate>> GetAvailableRatesForOrderAsync(string cityName, decimal weight);
    }
}