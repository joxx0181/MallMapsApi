using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MallMapsApi.Controllers
{
    [ApiController]
    [Route("Mall")]
    public class MallController : ControllerBase
    {
        [HttpGet("Malls")]
        public IActionResult GetAllMallsByCVRNR(int cvrnr) 
        {

            return Ok();
        }
    }
}
