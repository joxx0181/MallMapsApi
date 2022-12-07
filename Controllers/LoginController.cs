using Microsoft.AspNetCore.Mvc;
using MallMapsApi.Interface;
using MallMapsApi.Controllers.Decorators;
using MallMapsApi.Utils;

namespace MallMapsApi.Controllers
{
    /// <summary>
    /// Login controller for login calls
    /// </summary>
    [ApiController]
    [Route("Validate")]
    public class LoginController : Controller
    {
        /// <summary>
        /// IVerify handles the logic and call toward dbhandler
        /// </summary>
        private readonly IVerify _verify;
        /// <summary>
        /// Create an instance of LoginController and inject IVerify
        /// </summary>
        /// <param name="verify"></param>
        public LoginController(IVerify verify)
        {
            _verify = verify;
        }

        /// <summary>
        /// User login post method
        /// </summary>
        /// <param name="uname">username</param>
        /// <param name="password">password</param>
        /// <returns>Session on success, BadRequest on failure</returns>
        [HttpPost("Login")]
        public IActionResult Login(string uname, string password)
        {
            try
            {
                //Check for null or empty values
                if (uname.IsStringNullOrWhiteSpace() || password.IsStringNullOrWhiteSpace())
                    return BadRequest("Username or password was empty");
                //Decorate sessionUser.
                SessionUserDecorator sessionUser = _verify.Verifiy(uname, password);
                //Return bad request if null
                if (sessionUser == null)
                    return BadRequest("Username or password was wrong");
                //Return ok if everything checks out
                return Ok(sessionUser);
            }
            catch (Exception e)
            {
                return BadRequest("Exception was hit" + e.Message);
            }
        }
        /// <summary>
        /// Create and insert an user, firm need to exsist to add user
        /// </summary>
        /// <param name="uname">username</param>
        /// <param name="password">user password</param>
        /// <param name="role">the role of the user</param>
        /// <param name="firmid">the firm user belong to</param>
        /// <returns></returns>
        [HttpPost("Create")]
        public IActionResult Create(string uname, string password, string role, int firmid)
        {
            try
            {
                //Check for null or empty values
                if (uname.IsStringNullOrWhiteSpace() || password.IsStringNullOrWhiteSpace() || role.IsStringNullOrWhiteSpace() || DataHelper.CVRIsNotValid(firmid))
                    return BadRequest("Values was not inserted correct");
                
                //return result 
                return Ok(_verify.CreateUser(uname, password, role, firmid));

            }
            catch (Exception e)
            {
                //Return execption message
                return BadRequest("Exception was hit" + e.Message);
            }
        }

        //TODO : Remove before final release
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok(_verify.GetUsers());
        }
    }
}
