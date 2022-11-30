using Microsoft.AspNetCore.Mvc;
using MallMapsApi.Interface;
using MallMapsApi.Controllers.Decorators;
using MallMapsApi.Utils;

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
        public IActionResult Login(string uName, string password)
        {
            try
            {
                if (uName.IsStringNullOrWhiteSpace() || password.IsStringNullOrWhiteSpace())
                    return BadRequest("Username or password was empty");

                SessionUserDecorator sessionUser = _verify.Verifiy(uName, password);
                if (sessionUser == null)
                    return BadRequest("Username or password was wrong");

                return Ok(sessionUser);
            }
            catch (Exception e)
            {
                return BadRequest("Exception was hit" + e.Message);
            }
        }

 

        [HttpPost("Create")]
        public IActionResult Create(string username, string password, string role, int firmid)
        {
            try
            {
                if (username.IsStringNullOrWhiteSpace() || password.IsStringNullOrWhiteSpace() || role.IsStringNullOrWhiteSpace() || DataHelper.CVRNRIsValid(firmid))
                    return BadRequest("Values was not inserted correct");
                

                return Ok(_verify.CreateUser(username, password, role, firmid));

            }
            catch (Exception e)
            {
                return BadRequest("Exception was hit" + e.Message);
            }
        }


        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok(_verify.GetUsers());
        }
    }
}
