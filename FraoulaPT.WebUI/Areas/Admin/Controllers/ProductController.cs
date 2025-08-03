using FraoulaPT.DTOs.ProductDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FraoulaPT.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: Admin/Product
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            var productDTOs = products.Adapt<List<ProductDetailDTO>>();
            
            // Kategori adlarını ayarlayalım
            foreach (var dto in productDTOs)
            {
                dto.CategoryName = GetCategoryName(dto.Category);
            }
            
            return View(productDTOs);
        }

        // GET: Admin/Product/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var productDTO = product.Adapt<ProductDetailDTO>();
            productDTO.CategoryName = GetCategoryName(productDTO.Category);
            
            return View(productDTO);
        }

        // GET: Admin/Product/Create
        public IActionResult Create()
        {
            ViewBag.Categories = GetCategorySelectList();
            return View();
        }

        // POST: Admin/Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateDTO productCreateDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _productService.AddAsync(productCreateDTO);
                    
                    TempData["Success"] = "Ürün başarıyla oluşturuldu!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Ürün oluşturulurken bir hata oluştu: " + ex.Message;
                }
            }

            ViewBag.Categories = GetCategorySelectList();
            return View(productCreateDTO);
        }

        // GET: Admin/Product/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var productDTO = product.Adapt<ProductUpdateDTO>();
            ViewBag.Categories = GetCategorySelectList();
            
            return View(productDTO);
        }

        // POST: Admin/Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductUpdateDTO productUpdateDTO)
        {
            if (id != productUpdateDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productService.UpdateAsync(productUpdateDTO);
                    
                    TempData["Success"] = "Ürün başarıyla güncellendi!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Ürün güncellenirken bir hata oluştu: " + ex.Message;
                }
            }

            ViewBag.Categories = GetCategorySelectList();
            return View(productUpdateDTO);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                {
                    return Json(new { success = false, message = "Ürün bulunamadı!" });
                }

                await _productService.DeleteAsync(id);
                return Json(new { success = true, message = "Ürün başarıyla silindi!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Ürün silinirken bir hata oluştu: " + ex.Message });
            }
        }

        // POST: Admin/Product/UpdateStock
        [HttpPost]
        public async Task<IActionResult> UpdateStock(Guid id, int quantity)
        {
            try
            {
                var result = await _productService.UpdateStockAsync(id, quantity);
                if (result)
                {
                    return Json(new { success = true, message = "Stok başarıyla güncellendi!" });
                }
                return Json(new { success = false, message = "Ürün bulunamadı!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Stok güncellenirken bir hata oluştu: " + ex.Message });
            }
        }

        // POST: Admin/Product/UpdatePrice
        [HttpPost]
        public async Task<IActionResult> UpdatePrice(Guid id, decimal price)
        {
            try
            {
                var result = await _productService.UpdatePriceAsync(id, price);
                if (result)
                {
                    return Json(new { success = true, message = "Fiyat başarıyla güncellendi!" });
                }
                return Json(new { success = false, message = "Ürün bulunamadı!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Fiyat güncellenirken bir hata oluştu: " + ex.Message });
            }
        }

        // Helper Methods
        private string GetCategoryName(ProductCategory category)
        {
            return category switch
            {
                ProductCategory.Clothing => "Giyim",
                ProductCategory.Supplement => "Supplement",
                ProductCategory.Accessory => "Aksesuar",
                ProductCategory.Equipment => "Ekipman",
                _ => "Bilinmeyen"
            };
        }

        private List<(ProductCategory Value, string Text)> GetCategorySelectList()
        {
            return new List<(ProductCategory, string)>
            {
                (ProductCategory.Clothing, "Giyim"),
                (ProductCategory.Supplement, "Supplement"),
                (ProductCategory.Accessory, "Aksesuar"),
                (ProductCategory.Equipment, "Ekipman")
            };
        }
    }
}