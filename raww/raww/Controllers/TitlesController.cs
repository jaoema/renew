using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataserviceLib;
using AutoMapper;
using raww.Models.Profiles;
using Microsoft.AspNetCore.Routing;
using raww.Models;

namespace raww.Controllers
{
    [ApiController]
    public class TitlesController : ControllerBase
    {
        private readonly IMapper _mapper;
        public TitlesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("api/movie/{tconst}", Name = nameof(GetMovie))]
        public IActionResult GetMovie(string tconst)
        {
            var ds = new Dataservice();
            var movie = ds.GetTitle(tconst);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TitleDto>(movie));
        }
        [HttpGet("api/similarmovies/{id}")]
        public IActionResult FindSimilarTitles(string id)
        {
            var ds = new Dataservice();
            var searchresult = ds.GetSimilarTitles(id);

            if (searchresult == null)
            {
                return NotFound();
            }

            return Ok(searchresult);
        }

    }
}
