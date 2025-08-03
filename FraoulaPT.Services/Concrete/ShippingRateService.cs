using FraoulaPT.Core.Abstracts;
using FraoulaPT.DTOs.ShippingRateDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace FraoulaPT.Services.Concrete
{
    public class ShippingRateService : BaseService<ShippingRate, ShippingRateDetailDTO, ShippingRateDetailDTO, ShippingRateCreateDTO, ShippingRateUpdateDTO>, IShippingRateService
    {
        public ShippingRateService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<ShippingRate>> GetByCityAsync(string cityName)
        {
            return await _unitOfWork.GetRepository<ShippingRate>()
                .Query()
                .Where(sr => sr.CityName.ToLower().Contains(cityName.ToLower()) && sr.IsActive)
                .ToListAsync();
        }

        public async Task<List<ShippingRate>> GetByCompanyAsync(string companyName)
        {
            return await _unitOfWork.GetRepository<ShippingRate>()
                .Query()
                .Where(sr => sr.CompanyName.ToLower().Contains(companyName.ToLower()) && sr.IsActive)
                .ToListAsync();
        }

        public async Task<List<ShippingRate>> GetActiveRatesAsync()
        {
            return await _unitOfWork.GetRepository<ShippingRate>()
                .Query()
                .Where(sr => sr.IsActive)
                .OrderBy(sr => sr.CityName)
                .ThenBy(sr => sr.CompanyName)
                .ToListAsync();
        }

        public async Task<decimal> CalculateShippingFeeAsync(string cityName, string companyName, decimal weight)
        {
            var rate = await _unitOfWork.GetRepository<ShippingRate>()
                .Query()
                .FirstOrDefaultAsync(sr => 
                    sr.CityName.ToLower() == cityName.ToLower() && 
                    sr.CompanyName.ToLower() == companyName.ToLower() && 
                    sr.IsActive && 
                    weight <= sr.MaxWeight);

            if (rate == null)
                return 0; // Kargo ücreti bulunamadı

            return rate.BasePrice + (rate.PricePerKg * weight);
        }

        public async Task<List<ShippingRate>> GetAvailableRatesForOrderAsync(string cityName, decimal weight)
        {
            return await _unitOfWork.GetRepository<ShippingRate>()
                .Query()
                .Where(sr => 
                    sr.CityName.ToLower() == cityName.ToLower() && 
                    sr.IsActive && 
                    weight <= sr.MaxWeight)
                .OrderBy(sr => sr.BasePrice + (sr.PricePerKg * weight))
                .ToListAsync();
        }
    }
}