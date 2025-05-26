using Microsoft.AspNetCore.Mvc;

namespace DevMatch.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
