using Microsoft.AspNetCore.Mvc;

namespace MallMapsApi.Controllers
{
    [ApiController]
    [Route("Map")]
    public class MapController : Controller
    {
        [HttpGet("Get")]
        public IActionResult GetByLocation()
        {
            return Ok();
        }
        [HttpPost("Update")]
        public IActionResult Update()
        {
            return Ok();
        }
        [HttpPost("Create")]
        public IActionResult Create()
        {
            return Ok();
        }
        [HttpDelete("Delete")]
        public IActionResult Delete()
        {
            return Ok();
        }
    }
}
