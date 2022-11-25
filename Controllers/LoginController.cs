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
                if (DataHelper.IsStringNullOrWhiteSpace(uName) || DataHelper.IsStringNullOrWhiteSpace(password))
                    return BadRequest("Username or password was empty");

                SessionUserDecorator sessionUser = _verify.Verifiy(uName, password);
                if (DataHelper.IsStringNullOrWhiteSpace(sessionUser.SessionKey) || DataHelper.IsStringNullOrWhiteSpace(sessionUser.Role))
                    return BadRequest("Sessionkey or role was empty");

                return Ok(sessionUser);
            }
            catch (Exception e)
            {
                return BadRequest("Exception was hit" + e.Message);
            }
        }

        [HttpPost("Create")]

        public IActionResult Create(string uName, string password, string role, int firmid)
        {
            try
            {
                if (DataHelper.IsStringNullOrWhiteSpace(uName) || DataHelper.IsStringNullOrWhiteSpace(password) || DataHelper.IsStringNullOrWhiteSpace(role) || DataHelper.CVRNRIsValid(firmid))
                    return BadRequest("Values was not inserted correct");
                return Ok(_verify.CreateUser(uName, password, role, firmid));

            }
            catch (Exception e)
            {
                return BadRequest("Exception was hit" + e.Message);
            }
        }
    }
}
