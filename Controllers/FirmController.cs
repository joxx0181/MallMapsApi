using MallMapsApi.Data.DTO;
using MallMapsApi.Interface;
using MallMapsApi.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MallMapsApi.Controllers
{
    // Firm controller for api call
    [ApiController]
    [Route("Firm")]
    public class FirmController : ControllerBase
    {
        // IFirm handling logic
        private readonly IFirm _firm;

        // Constructor declartion and and inject IFirm
        public FirmController(IFirm firm)
        {
            _firm = firm;
        }



        // Create and insert firm in database 
        [HttpPost("Create")]
        public IActionResult Create(int cvr, string name)
        {
            // handle exceptions using try-catch
            try
            {

                // Return a badrequest
                if (name.IsStringNullOrWhiteSpace() || DataHelper.CVRIsNotValid(cvr))
                    return BadRequest("Null or whitespace values are not allowed");

                // Calling IFirm for creating new firm and return ok result
                return Ok(_firm.CreateFirm(cvr, name));

            }
            catch (Exception ex)
            {

                // Return Execption
                return BadRequest(ex.Message);
            }
            
        }
    }
}


