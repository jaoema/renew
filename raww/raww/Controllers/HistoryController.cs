using DataserviceLib;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace raww.Controllers
{
    [ApiController]
    public class HistoryController : ControllerBase
    {
        [HttpGet("api/actorsearch/{searchstring}")]
        public IActionResult ActorSearch(string searchstring)
        {
            var ds = new Dataservice();
            var searchresult = ds.FindActor(searchstring);

            if (searchresult == null)
            {
                return NotFound();
            }

            return Ok(searchresult);
        }
    }
}
