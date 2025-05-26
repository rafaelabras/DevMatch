using Microsoft.AspNetCore.Mvc;

namespace DevMatch.Controllers
{
    public class MentorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
