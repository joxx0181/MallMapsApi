using Microsoft.AspNetCore.Mvc;

namespace MallMapsApi.Controllers
{
    [ApiController]
    [Route("Validate")]
    public class LoginController : Controller
    {
        [HttpPost("Login")]
        public IActionResult Login(string test)
        {
            return Ok("Du er logget ind " + test);
        }
    }
}
