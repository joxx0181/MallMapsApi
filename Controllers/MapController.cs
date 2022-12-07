using MallMapsApi.Controllers.Views;
using MallMapsApi.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MallMapsApi.Controllers
{
    /// <summary>
    /// Map controller for api calls
    /// </summary>
    [ApiController]
    [Route("Map")]
    public class MapController : Controller
    {
        private readonly IMap _map;
        /// <summary>
        /// Creates an instance of MapController with IMap injection
        /// </summary>
        /// <param name="map"></param>
        public MapController(IMap map)
        {
            _map = map;
        }

        /// <summary>
        /// Get the map from mallID
        /// </summary>
        /// <param name="mallId"></param>
        /// <returns></returns>
        [HttpGet("Get")]
        public IActionResult GetByMallId(int mallId)
        {
            try
            {
                //Calling Handler that fetch and decorate maps
                var res = _map.GetMapsByLocation(mallId);
                //if none found return badrequest
                if (res == null || res.Count() == 0)
                    return BadRequest(res);
                //return collection of maps 
                return Ok(res);
            }
            catch (Exception e)
            {
                //return BadRequest with Exception message
                return BadRequest("Exception was hit: " + e.Message);
            }
        }

        /// <summary>
        /// Map post call
        /// </summary>
        /// <param name="map">Decorator map</param>
        /// <returns></returns>
        [HttpPost("Create")]
        public IActionResult Create(MapV map)
        {
            try
            {
                //If map is not null call IMap and create map and return result
                if (map != null)
                    return Ok(_map.CreateMap(map));
                return BadRequest("Data was null");

            }
            catch (Exception e)
            {
                //return BadRequest with Exception message
                return BadRequest("Exception was hit: " + e.Message);
            }
        }
    }
}
