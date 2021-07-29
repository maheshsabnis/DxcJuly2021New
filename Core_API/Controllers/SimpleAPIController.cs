using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_API.Controllers
{
    [Route("api/[controller]")]
  // [ApiController]
    public class SimpleAPIController : ControllerBase
    {
        private List<string> Names = new List<string>() 
        {
             "James Bond", "Ethan Hunt", "Indiana Jones", "Jason Bourn"
        }; 
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Names);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(Names[id]);
        }
        //[HttpPost]
        //public IActionResult Post([FromBody]Character character)
        //{
        //    Names.Add(character.Name);
        //    return Ok(Names);
        //}

       // [HttpPost]
        // public IActionResult Post(string name)
        //public IActionResult Post([FromQuery]Character c)
        // {
        //     // an additional code for Storing the Querystring in CLR object
        //     //Character c = new Character()
        //     //{
        //     //    Name = name
        //     //};
        //     Names.Add(c.Name);
        //     return Ok(Names);
        // }
        [HttpPost("{name}")]
        public IActionResult Post([FromRoute] Character c)
        {
            // an additional code for Storing the Querystring in CLR object
            //Character c = new Character()
            //{
            //    Name = name
            //};
            Names.Add(c.Name);
            return Ok(Names);
        }
    }

    public class Character
    {
        public string Name { get; set; }
    }
}
