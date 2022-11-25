using MallMapsApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace MallMapsApi.Controllers
{
    [ApiController]
    [Route("Component")]
    public class ComponentController : Controller
    {
        [HttpGet("Get")]
        public IActionResult GetById()
        {
            return Ok();
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok();
        }
    }
}
