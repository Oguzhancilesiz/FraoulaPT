using FraoulaPT.Core.Enums;
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
        protected void ShowAlert(string title, string message, AlertType icon = AlertType.success)
        {
            TempData["AlertTitle"] = title;
            TempData["AlertMessage"] = message;
            TempData["AlertIcon"] = icon; // success, error, warning, info, question
        }
    }
}
