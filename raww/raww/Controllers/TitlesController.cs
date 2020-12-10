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

            movie.Tconst = movie.Tconst.Trim();
            var mapped = _mapper.Map<TitleDto>(movie);
            
            mapped.Bookmarklink = Url.Link(nameof(BookmarkController.Bookmark), new { id = movie.Tconst , movie=true});
            mapped.Ratelink = Url.Link(nameof(Rate), new { movie.Tconst, rating = 5 });

            
            return Ok(mapped);
        }
        [HttpGet("api/specificmovie/{tconst}", Name = nameof(GetSpecificMovie))]
        public IActionResult GetSpecificMovie(string tconst)
        {
            var ds = new Dataservice();
            var searchresult = ds.GetSpecificMovie(tconst);

            if (searchresult is null)
            {
                return NotFound();
            }

           // var populatedresult = CreateSpecificMovieResult(page, pagesize, searchresult);
            return Ok(searchresult);
            //return Ok();
        }

        [HttpPost("api/movie/rate/{username}/{tconst}/{rating}", Name = nameof(Rate))]
        public IActionResult Rate(string username, string tconst, int rating)
        {
            var ds = new Dataservice();
            var success = ds.Rate(username, tconst, rating);

            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
