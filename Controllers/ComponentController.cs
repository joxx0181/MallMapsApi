using MallMapsApi.Data;
using MallMapsApi.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MallMapsApi.Controllers
{
    [ApiController]
    [Route("Component")]
    public class ComponentController : Controller
    {
        private IComponent _component;

        public ComponentController(IComponent component)
        {
            _component = component;
        }

        [HttpGet("Get")]
        public IActionResult GetById(int mapid)
        {
            try
            {
                if (mapid == int.MaxValue)
                    return BadRequest("Wrong input");
                if (mapid < 0)
                    return BadRequest("Wrong input");

                Dictionary<string, object> data = _component.GetById(mapid);
                if (data == null)
                    return BadRequest("There was no data, with that mapid");

                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest("Execption was hit: " + e.Message);
            }
        }
    }
}
