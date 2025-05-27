using Microsoft.AspNetCore.Mvc;

namespace DevMatch.Controllers
{
    public class RatingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
