using FraoulaPT.WebUI.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FraoulaPT.WebUI.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
