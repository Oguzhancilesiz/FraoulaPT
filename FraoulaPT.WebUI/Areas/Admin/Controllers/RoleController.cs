using FraoulaPT.DTOs.AppRoleDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FraoulaPT.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin,SuperAdmin")]
    public class RoleController : BaseController
    {
        private readonly IAppRoleService _roleService;
        private readonly RoleManager<AppRole> _roleManager;

        public RoleController(IAppRoleService roleService, RoleManager<AppRole> roleManager)
        {
            _roleService = roleService;
            _roleManager = roleManager;
        }

        // Tüm Rolleri Listele
        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAllAsync();
            return View(roles);
        }

        // Yeni Rol Ekle - GET
        public IActionResult Create()
        {
            return View();
        }

        // Yeni Rol Ekle - POST
        [HttpPost]
        public async Task<IActionResult> Create(AppRoleCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            // Eğer AppRole IdentityRole'den miras alıyorsa:
            var role = new AppRole
            {
                Name = dto.Name,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                Status = Core.Enums.Status.Active,

            };

            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
                return RedirectToAction("Index");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);
            return View(dto);
        }

        // Rol Düzenle - GET
        public async Task<IActionResult> Edit(Guid id)
        {
            var role = await _roleService.GetByIdAsync(id);
            return View(role);
        }

        // Rol Düzenle - POST
        [HttpPost]
        public async Task<IActionResult> Edit(AppRoleUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _roleService.UpdateAsync(dto);
            return RedirectToAction("Index");
        }
    }
}
