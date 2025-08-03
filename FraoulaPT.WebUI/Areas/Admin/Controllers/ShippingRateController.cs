using FraoulaPT.DTOs.ShippingRateDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FraoulaPT.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ShippingRateController : Controller
    {
        private readonly IShippingRateService _shippingRateService;

        public ShippingRateController(IShippingRateService shippingRateService)
        {
            _shippingRateService = shippingRateService;
        }

        // GET: Admin/ShippingRate
        public async Task<IActionResult> Index()
        {
            var rates = await _shippingRateService.GetActiveRatesAsync();
            var rateDTOs = rates.Adapt<List<ShippingRateDetailDTO>>();
            return View(rateDTOs);
        }

        // GET: Admin/ShippingRate/Create
        public IActionResult Create()
        {
            ViewBag.Companies = GetCompanySelectList();
            ViewBag.Cities = GetCitySelectList();
            return View();
        }

        // POST: Admin/ShippingRate/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShippingRateCreateDTO createDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _shippingRateService.AddAsync(createDTO);
                    
                    TempData["Success"] = "Kargo ücreti başarıyla oluşturuldu!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Kargo ücreti oluşturulurken bir hata oluştu: " + ex.Message;
                }
            }

            ViewBag.Companies = GetCompanySelectList();
            ViewBag.Cities = GetCitySelectList();
            return View(createDTO);
        }

        // GET: Admin/ShippingRate/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var rate = await _shippingRateService.GetByIdAsync(id);
            if (rate == null)
            {
                return NotFound();
            }

            var rateDTO = rate.Adapt<ShippingRateUpdateDTO>();
            ViewBag.Companies = GetCompanySelectList();
            ViewBag.Cities = GetCitySelectList();
            
            return View(rateDTO);
        }

        // POST: Admin/ShippingRate/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ShippingRateUpdateDTO updateDTO)
        {
            if (id != updateDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _shippingRateService.UpdateAsync(updateDTO);
                    
                    TempData["Success"] = "Kargo ücreti başarıyla güncellendi!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Kargo ücreti güncellenirken bir hata oluştu: " + ex.Message;
                }
            }

            ViewBag.Companies = GetCompanySelectList();
            ViewBag.Cities = GetCitySelectList();
            return View(updateDTO);
        }

        // POST: Admin/ShippingRate/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var rate = await _shippingRateService.GetByIdAsync(id);
                if (rate == null)
                {
                    return Json(new { success = false, message = "Kargo ücreti bulunamadı!" });
                }

                await _shippingRateService.DeleteAsync(id);
                return Json(new { success = true, message = "Kargo ücreti başarıyla silindi!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Kargo ücreti silinirken bir hata oluştu: " + ex.Message });
            }
        }

        // API: Şehre göre kargo ücretlerini getir
        [HttpGet]
        public async Task<IActionResult> GetRatesByCity(string cityName)
        {
            try
            {
                var rates = await _shippingRateService.GetByCityAsync(cityName);
                return Json(new { success = true, data = rates });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // API: Kargo ücreti hesapla
        [HttpPost]
        public async Task<IActionResult> CalculateFee(string cityName, string companyName, decimal weight)
        {
            try
            {
                var fee = await _shippingRateService.CalculateShippingFeeAsync(cityName, companyName, weight);
                return Json(new { success = true, fee = fee });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Helper Methods
        private List<string> GetCompanySelectList()
        {
            return new List<string>
            {
                "Yurtiçi Kargo",
                "MNG Kargo",
                "PTT Kargo",
                "Aras Kargo",
                "Sürat Kargo",
                "UPS Kargo"
            };
        }

        private List<string> GetCitySelectList()
        {
            return new List<string>
            {
                "İstanbul", "Ankara", "İzmir", "Bursa", "Antalya", "Adana", "Konya", "Gaziantep",
                "Şanlıurfa", "Kocaeli", "Mersin", "Diyarbakır", "Hatay", "Manisa", "Kayseri",
                "Samsun", "Balıkesir", "Kahramanmaraş", "Van", "Aydın", "Denizli", "Batman",
                "Tekirdağ", "Muğla", "Elazığ", "Sivas", "Tokat", "Erzurum", "Sakarya", "Edirne"
            };
        }
    }
}