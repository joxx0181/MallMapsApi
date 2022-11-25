using Microsoft.AspNetCore.Mvc;
using MallMapsApi.DTO;
using MallMapsApi.Interface;

namespace MallMapsApi.Controllers
{
    [ApiController]
    [Route("Validate")]
    public class LoginController : Controller
    {
        private readonly IVerify _verify;

        public LoginController(IVerify verify)
        {
            _verify = verify;
        }

        [HttpPost("Login")]
        public IActionResult Login(FirmUser user)
        {
            _verify.Verifiy(user);
            return Ok();
        }

 

        [HttpPost("Create")]
        public  IActionResult Create(FirmUser user)
        {
            _verify.CreateUser(user);
            return Ok();
        }
    }
}
