using FraoulaPT.Core.Abstracts;
using FraoulaPT.DTOs.ProductDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace FraoulaPT.Services.Concrete
{
    public class ProductService : BaseService<Product, ProductDetailDTO, ProductDetailDTO, ProductCreateDTO, ProductUpdateDTO>, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<Product>> GetByCategoryAsync(ProductCategory category)
        {
            return await _unitOfWork.GetRepository<Product>()
                .Query()
                .Where(p => p.Category == category)
                .ToListAsync();
        }

        public async Task<List<Product>> GetFeaturedProductsAsync()
        {
            return await _unitOfWork.GetRepository<Product>()
                .Query()
                .Where(p => p.IsFeatured)
                .ToListAsync();
        }

        public async Task<List<Product>> GetInfluencerChoicesAsync()
        {
            return await _unitOfWork.GetRepository<Product>()
                .Query()
                .Where(p => p.IsInfluencerChoice)
                .ToListAsync();
        }

        public async Task<Product?> GetBySlugAsync(string slug)
        {
            return await _unitOfWork.GetRepository<Product>()
                .Query()
                .FirstOrDefaultAsync(p => p.Slug == slug);
        }

        public async Task<bool> UpdateStockAsync(Guid productId, int quantity)
        {
            var product = await _unitOfWork.GetRepository<Product>().GetById(productId);
            if (product == null) return false;

            product.StockQuantity = quantity;
            await _unitOfWork.GetRepository<Product>().Update(product);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatePriceAsync(Guid productId, decimal price)
        {
            var product = await _unitOfWork.GetRepository<Product>().GetById(productId);
            if (product == null) return false;

            product.Price = price;
            await _unitOfWork.GetRepository<Product>().Update(product);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<List<Product>> SearchProductsAsync(string searchTerm)
        {
            return await _unitOfWork.GetRepository<Product>()
                .Query()
                .Where(p => p.Name.Contains(searchTerm) || 
                           p.Description.Contains(searchTerm))
                .ToListAsync();
        }
    }
}