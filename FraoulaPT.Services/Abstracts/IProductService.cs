using FraoulaPT.Core.Abstracts;
using FraoulaPT.DTOs.ProductDTOs;
using FraoulaPT.Entity;

namespace FraoulaPT.Services.Abstracts
{
    public interface IProductService : IBaseService<ProductDetailDTO, ProductDetailDTO, ProductCreateDTO, ProductUpdateDTO>
    {
        Task<List<Product>> GetByCategoryAsync(ProductCategory category);
        Task<List<Product>> GetFeaturedProductsAsync();
        Task<List<Product>> GetInfluencerChoicesAsync();
        Task<Product?> GetBySlugAsync(string slug);
        Task<bool> UpdateStockAsync(Guid productId, int quantity);
        Task<bool> UpdatePriceAsync(Guid productId, decimal price);
        Task<List<Product>> SearchProductsAsync(string searchTerm);
    }
}