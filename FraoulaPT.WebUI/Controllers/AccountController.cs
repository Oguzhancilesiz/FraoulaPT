using FraoulaPT.DTOs.UserDTOs;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.WebUI.Models.Enums;
using FraoulaPT.WebUI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FraoulaPT.WebUI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _userService.RegisterAsync(model);
                ShowMessage("Kayıt başarılı! Lütfen giriş yapınız.", MessageType.Success);
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, MessageType.Error);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _userService.LoginAsync(model);
                ShowMessage("Giriş Başarılı!", MessageType.Success);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ShowMessage("Hata : " + ex.Message, MessageType.Error);
                return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _userService.SignOutAsync();
            ShowMessage("Güle Güle :)", MessageType.Info);
            return RedirectToAction("Login");
        }
    }

}
