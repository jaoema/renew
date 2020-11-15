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
            var trimmedtconst = movie.Tconst.Trim();
            mapped.Bookmarklink = Url.Link(nameof(BookmarkController.Bookmark), new { trimmedtconst , movie=true});
            
            return Ok(mapped);
        }

    }
}
