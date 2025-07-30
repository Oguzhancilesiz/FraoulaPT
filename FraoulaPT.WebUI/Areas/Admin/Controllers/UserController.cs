using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.AppUserDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.Services.Concrete;
using FraoulaPT.WebUI.Areas.Admin.Models.ViewModels.AccountViewModels;
using FraoulaPT.WebUI.Controllers;
using FraoulaPT.WebUI.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FraoulaPT.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Coach")]
    public class UserController : BaseController
    {
        private readonly IAppUserService _userService;
        private readonly IUserProfileService _userProfileService;
        private readonly IAppRoleService _roleService;
        private readonly UserManager<AppUser> _userManager;

        public UserController(IAppUserService userService, IAppRoleService roleService,UserManager<AppUser> userManager, IUserProfileService userProfileService)
        {
            _userService = userService;
            _roleService = roleService;
            _userManager = userManager;
            _userProfileService = userProfileService;
        }

        // Kullanıcı Listesi
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllAsync();
            var list = new List<AdminUserListVM>();
            foreach (var u in users)
            {
                var roles = await _userService.GetUserRolesAsync(u.Id);
                list.Add(new AdminUserListVM
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FullName = u.FullName,
                    Email = u.Email,
                    IsActive = u.Status == FraoulaPT.Core.Enums.Status.Active,
                    Roles = string.Join(", ", roles)
                });
            }
            return View(list);
        }



        // Kullanıcı Detayı (isteğe bağlı)
        public async Task<IActionResult> Detail(Guid id)
        {
            var user = await _userService.GetByIdAsync(id); // AppUserDetailDTO döner
            var profile = await _userProfileService.GetByAppUserIdAsync(id); // UserProfileDetailDTO döner
            var roles = await _userService.GetUserRolesAsync(id);

            var vm = new UserFullDetailVM
            {
                User = user,
                Profile = profile,
                Roles = roles
            };

            return View(vm);
        }
        //public async Task<IActionResult> Detail(Guid id)
        //{
        //    var user = await _userService.GetByIdAsync(id);
        //    var roles = await _userService.GetUserRolesAsync(id);
        //    // Dilersen detay VM'e mapleyebilirsin.
        //    ViewBag.Roles = roles;
        //    return View(user);
        //}

        // Kullanıcıyı Aktif/Pasif Yap
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return NotFound();

            // Status toggle
            user.Status = user.Status == Status.Active ? Status.DeActive : Status.Active;

            await _userManager.UpdateAsync(user);
            ShowMessage("Kullanıcı durumu güncellendi.", user.Status == Status.Active ? MessageType.Success : MessageType.Warning);
            return RedirectToAction("Index");
        }
        // ROL ATA (GET)
        [HttpGet]
        public async Task<IActionResult> RoleAssign(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            var allRoles = await _roleService.GetAllAsync();
            var assignedRoles = await _userService.GetUserRolesAsync(id);

            var vm = new AdminUserRoleAssignVM
            {
                UserId = user.Id,
                UserName = user.UserName,
                AllRoles = allRoles.Select(r => r.Name).ToList(),
                AssignedRoles = assignedRoles.ToList()
            };

            return View(vm);
        }

        // ROL ATA (POST)
        [HttpPost]
        public async Task<IActionResult> RoleAssign(AdminUserRoleAssignVM vm)
        {
            await _userService.UpdateUserRolesAsync(vm.UserId, vm.AssignedRoles);
            ShowMessage("Kullanıcı rolleri güncellendi.", MessageType.Success);
            return RedirectToAction("Index");
        }
    }
}
