using MallMapsApi.Interface;
using MallMapsApi.Utils;
using Microsoft.AspNetCore.Mvc;

namespace MallMapsApi.Controllers
{
    [ApiController]
    [Route("Mall")]
    public class MallController : ControllerBase
    {
        private readonly IMall mall;

        public MallController(IMall mall)
        {
            this.mall = mall;
        }

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

        [HttpPost("Create")]
        public IActionResult Create(int firmid, string location)
        {
            try
            {
                if (location.IsStringNullOrWhiteSpace() || DataHelper.CVRIsNotValid(firmid))
                    throw new Exception("Values was not inserted correct");

                return Ok(mall.CreateMall(firmid, location));

            }
            catch (Exception e)
            {
                return BadRequest("Exception was hit" + e.Message);
            }
        }

    }
}
