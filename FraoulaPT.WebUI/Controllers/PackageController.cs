using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FraoulaPT.WebUI.Controllers
{
    public class PackageController : Controller
    {
        private readonly IPackageService _packageService;

        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        // /Package/Index
        public async Task<IActionResult> Index()
        {
            var packages = await _packageService.GetActivePackagesAsync();
            return View(packages); // List<PackageListDTO> olarak gönderiyoruz
        }
        public async Task<IActionResult> Buy(Guid id)
        {
            var package = await _packageService.GetByIdAsync(id);
            if (package == null)
                return NotFound();

            // İleride burada ödeme yöntemlerini gösterip, seçim aldıktan sonra satın alma akışına yönlendireceğiz
            return View(package);
        }
    }

}
