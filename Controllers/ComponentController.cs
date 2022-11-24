using MallMapsApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace MallMapsApi.Controllers
{
    [ApiController]
    [Route("Component")]
    public class ComponentController : Controller
    {
        private readonly ITest _test;
        public ComponentController(ITest test)
        {
            _test = test;
        }
        [HttpGet("Get")]
        public IActionResult GetById()
        {
            _test.Test();
            return Ok();
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok();
        }
    }
}
