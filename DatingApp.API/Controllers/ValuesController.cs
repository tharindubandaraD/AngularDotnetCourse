using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace DatingApp.API.Controllers
{
    //routing infromation in here - apicontroller is a validater then we use need a route
    //authorization
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            _context = context;
        }
        // GET api/values
        //iEnumearable in this case is collection of strings 
        [HttpGet]
        //public ActionResult<IEnumerable<string>> Get() - code
        public async Task<IActionResult> GetValues()
        {
            //throw new Exception("test exception");
           // return new string[] { "value1", "value3" };
           var values = await _context.Values.ToListAsync();
           return Ok(values);
        }

        /*IActionResult is an interface, we can create a 
        custom response as a return, when you use ActionResult 
        you can return only predefined ones for returning a View or a resource
        --ViewResult, PartialViewResult, JsonResult, etc., derive from ActionResult.
        */
        //this is mention as anonymous
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValues(int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(value);
        }


        // GET api/values/5
        /*[HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "tharindu";
        }*/

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