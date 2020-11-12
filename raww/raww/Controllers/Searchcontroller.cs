using DataserviceLib;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using raww.Models;

namespace raww.Controllers
{
    [ApiController]
    public class Searchcontroller : ControllerBase
    {
        [HttpGet("api/simplesearch")]
        public IActionResult SimpleSearch()
        {
            var ds = new Dataservice();
            var searchresult = ds.SimpleSearch("gjhe", 1, 50);

            if (!searchresult.Any())
            {
                return NotFound();
            }

            //searchresult = searchresult.Select(SearchDto);
            //var populatedresult = CreateResult(page, pagesize, searchresult);
            return Ok(searchresult);
        }
        [HttpGet("api/actorsearch/{searchstring}")]
        public IActionResult ActorSearch(string searchstring, int page, int pagesize)
        { 
            var ds = new Dataservice();
            var searchresult = ds.FindActor(searchstring, page, pagesize);

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
        private object CreateResult(int page, int pageSize, IList<SimpleSearch> titles)
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
