using MallMapsApi.Data;
using MallMapsApi.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MallMapsApi.Controllers
{
    /// <summary>
    /// Component controller for api
    /// </summary>
    [ApiController]
    [Route("Component")]
    public class ComponentController : Controller
    {
        /// <summary>
        /// IComponent handle all logic for components and calls dbHandler when needed
        /// </summary>
        private IComponent _component;
        /// <summary>
        /// Ceate instance of COmponentController and Inject IComponent
        /// </summary>
        /// <param name="component"></param>
        public ComponentController(IComponent component)
        {
            _component = component;
        }
        /// <summary>
        /// get up components from map id
        /// </summary>
        /// <param name="mapid">the map id that belongs to the components</param>
        /// <returns>Dictionary of components</returns>
        [HttpGet("Get")]
        public IActionResult GetById(int mapid)
        {
            try
            {
                //Wrong values check
                if (mapid == int.MaxValue)
                    return BadRequest("Wrong input");
                if (mapid < 0)
                    return BadRequest("Wrong input");
                //Get Components to return
                Dictionary<string, object> data = _component.GetById(mapid);
                //Null check if true return error
                if (data == null)
                    return BadRequest("There was no data, with that mapid");
                //return ok with data if everything checksout
                return Ok(data);
            }
            catch (Exception e)
            {
                //Return execption
                return BadRequest("Execption was hit: " + e.Message);
            }
        }
    }
}
