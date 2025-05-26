using Microsoft.AspNetCore.Mvc;

namespace DevMatch.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
