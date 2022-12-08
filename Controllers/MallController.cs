using MallMapsApi.Interface;
using MallMapsApi.Utils;
using Microsoft.AspNetCore.Mvc;

namespace MallMapsApi.Controllers
{
    /// <summary>
    /// Mall controller for api calls
    /// </summary>
    [ApiController]
    [Route("Mall")]
    public class MallController : ControllerBase
    {
        /// <summary>
        /// IMall that handle logic and calls DbHandler
        /// </summary>
        private readonly IMall mall;
        /// <summary>
        /// Create instance of MallController and inject IMall
        /// </summary>
        /// <param name="mall"></param>
        public MallController(IMall mall)
        {
            this.mall = mall;
        }
        /// <summary>
        /// Get malls from CVR number
        /// </summary>
        /// <param name="cvrnr"></param>
        /// <returns></returns>

        [HttpGet("GetMalls")]
        public IActionResult GetMalls(int cvrnr)
        {
            try
            {
                if (DataHelper.CVRIsNotValid(cvrnr))
                    throw new Exception("Cvrnr was not valid");
                var malls = mall.GetMalls(cvrnr);
                if (malls.Count() == 0)
                    throw new Exception("No malls with that CVRNR");
                return Ok(malls);
            }
            catch (Exception e)
            {
                return BadRequest("Exception was hit: " + e.Message);
            }


        }

        /// <summary>
        /// Create and insert mall 
        /// </summary>
        /// <param name="firmId"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public IActionResult Create(int firmId, string location)
        {
            try
            {
                //return badrequest if we retrieve null or whitespace values
                if (location.IsStringNullOrWhiteSpace() || DataHelper.CVRIsNotValid(firmId))
                    return BadRequest("Null or whitespace values are not allowed");
                //Calling IMall method create mall and return result
                return Ok(mall.CreateMall(firmId, location));

            }
            catch (Exception e)
            {
                //return Execption if hit
                return BadRequest("Exception was hit" + e.Message);
            }
        }

    }
}
