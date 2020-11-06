using DataserviceLib;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace raww.Controllers
{
    [ApiController]
    public class Searchcontroller : ControllerBase
    {
        [HttpGet("api/simplesearch/{searchstring}")]
        public IActionResult SimpleSearch(string searchstring)
        {
            var ds = new Dataservice();
            var searchresult = ds.SimpleSearch(searchstring);

            if (searchresult == null)
            {
                return NotFound();
            }

            return Ok(searchresult);
        }
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
        [HttpGet("api/coactorsearch/{searchstring}")]
        public IActionResult CoActorSearch(string searchstring)
        {
            var ds = new Dataservice();
            var searchresult = ds.FindCoActor(searchstring);

            if (searchresult == null)
            {
                return NotFound();
            }

            return Ok(searchresult);
        }
        private object CreateResult(int page, int pageSize, IList<Titlebasics> titles)
        {
            //var titles = titles.Select(CreateProductElementDto);

            var count = titles.Count();

            var navigationUrls = CreatePagingNavigation(page, pageSize, count);

            var result = new
            {
                navigationUrls.prev,
                navigationUrls.cur,
                navigationUrls.next,
                count,
                titles
            };
            return result;
        }
        private (string prev, string cur, string next) CreatePagingNavigation(int page, int pageSize, int count)
        {
            string prev = null;

            if (page > 0)
            {
                prev = Url.Link(nameof(SimpleSearch), new { page = page - 1, pageSize });
            }

            string next = null;

            if (page < (int)Math.Ceiling((double)count / pageSize) - 1)
                next = Url.Link(nameof(SimpleSearch), new { page = page + 1, pageSize });

            var cur = Url.Link(nameof(SimpleSearch), new { page, pageSize });

            return (prev, cur, next);
        }
    }
}
