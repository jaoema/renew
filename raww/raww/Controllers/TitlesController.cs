﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataserviceLib;

namespace raww.Controllers
{
    [ApiController]
    public class TitlesController : ControllerBase
    {
        [HttpGet("api/movie/{tconst}")]
        public IActionResult GetMovie(string tconst)
        {
            var ds = new Dataservice();
            var movie = ds.GetTitle(tconst);

            if (movie == null)
            {
                return NotFound();
            }
            
            return Ok(movie);
        }


    }
}