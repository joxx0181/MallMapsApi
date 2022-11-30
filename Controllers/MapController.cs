﻿using MallMapsApi.Controllers.Views;
using MallMapsApi.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MallMapsApi.Controllers
{
    [ApiController]
    [Route("Map")]
    public class MapController : Controller
    {
        private readonly IMap _map;
        public MapController(IMap map)
        {
            _map = map;
        }

        [HttpGet("Get")]
        public IActionResult GetByLocation(string location)
        {
            try
            {
                _map.GetMapsByLocation(location);
                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest("Exception was hit: " + e.Message);
            }
        }

        //[HttpPost("Update")]
        //public IActionResult Update()
        //{
        //    return Ok();
        //}

        [HttpPost("Create")]
        public IActionResult Create(MapV map)
        {
            try
            {
                if (map != null)
                    return Ok(_map.CreateMap(map));
                return BadRequest("Data was null");

            }
            catch (Exception e)
            {
                return BadRequest("Exception was hit: " + e.Message);
            }
        }
        //[HttpDelete("Delete")]
        //public IActionResult Delete()
        //{
        //    return Ok();
        //}
    }
}
