using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataserviceLib;
using SqlFunctions;

namespace raww.Controllers
{
    [ApiController]
    public class PersonController : ControllerBase
    {
        
        [HttpGet("api/{nconst}", Name = nameof(GetPerson))]
        public IActionResult GetPerson(string nconst)
        {
            var ds = new Dataservice();
            var result = ds.FindActor(nconst);

            return Ok(result);
        }
        /*
        [HttpGet("api/{search}", Name = nameof(SearchName))]
        public IActionResult SearchName(string search)
        {
            var ds = new Dataservice();
            var result = ds.SearchName(search);

            return Ok(result);
        }*/

    }
}
