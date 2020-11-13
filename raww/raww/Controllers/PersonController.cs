using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataserviceLib;

namespace raww.Controllers
{
    [ApiController]
    public class PersonController : ControllerBase
    {
        [HttpGet("api/{nconst}", Name = nameof(GetPerson))]
        public IActionResult GetPerson(string nconst)
        {
            var ds = new Dataservice();
            var result = ds.GetPerson(nconst);

            return Ok(result);
        }

    }
}
