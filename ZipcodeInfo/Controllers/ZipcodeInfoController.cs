using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZipcodeInfo.DomainClasses;
using ZipcodeInfo.Processors;

namespace ZipcodeInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZipcodeInfoController : ControllerBase
    {
        private IZipcodeInfoProcessor _zipcodeInfoProcessor;

        public ZipcodeInfoController(IZipcodeInfoProcessor zipcodeInfoProcessor)
        {
            _zipcodeInfoProcessor = zipcodeInfoProcessor;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Zipcode zipcode)
        {
            var apiResponse = new ApiResponse<CayuseZipcodeInfo>();
            try
            {
                apiResponse = await _zipcodeInfoProcessor.GenerateZipcodeInfoAsync(zipcode);
                if (apiResponse.IsValidationError)
                {
                    return BadRequest(apiResponse);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500);
                //todo log exception
            }

            return new JsonResult(apiResponse);
        }

    }
}
