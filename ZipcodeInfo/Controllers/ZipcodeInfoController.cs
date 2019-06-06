using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZipcodeInfo.DomainClasses;

namespace ZipcodeInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZipcodeInfoController : ControllerBase
    {
        IValidator<Zipcode> _zipcodeValidator;

        public ZipcodeInfoController(IValidator<Zipcode> zipcodeValidator)
        {
            _zipcodeValidator = zipcodeValidator;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        
        [HttpPost]
        public IActionResult Get(Zipcode zipcode)
        {

            try
            {
                var validateResponse = _zipcodeValidator.Validate(zipcode);
                
            }
            catch (Exception e)
            {
                return StatusCode(500);
                //todo log it
            }
            
                       
            return BadRequest(zipcode);
            
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
