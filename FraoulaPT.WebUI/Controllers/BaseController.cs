using FraoulaPT.WebUI.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FraoulaPT.WebUI.Controllers
{
    public class BaseController : Controller
    {
        public void ShowMessage(string message, MessageType messageType)
        {
            TempData["messageType"] = messageType.ToString();
            TempData["message"] = message;
        }
    }
}
