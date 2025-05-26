using DevMatch.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace DevMatch.Controllers
{
    [ApiController]
    [Route("devmatch/account")]
    public class UserController : Controller
    {
        [HttpPost("Register")]
        public IActionResult Register([FromBody]RegisterDto register)
        {


        }
    }
}
