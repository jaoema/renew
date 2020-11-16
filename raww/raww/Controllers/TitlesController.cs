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
            
            var mapped = _mapper.Map<TitleDto>(movie);
            movie.Tconst = movie.Tconst.Trim();
            mapped.Bookmarklink = Url.Action(nameof(BookmarkController.Bookmark), new { id = movie.Tconst , movie=true});
            mapped.Ratelink = Url.Link(nameof(Rate), new { movie.Tconst, rating = 5 });

            
            return Ok(mapped);
        }
        [HttpPost("api/movie/rate", Name = nameof(Rate))]
        public IActionResult Rate(string tconst, int rating)
        {
            var ds = new Dataservice();
            var success = ds.Rate(tconst, rating);

            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
