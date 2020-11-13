using DataserviceLib;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using raww.Models;
using AutoMapper;
using raww.Models.Profiles;

namespace raww.Controllers
{
    [ApiController]
    public class Searchcontroller : ControllerBase
    {
        private readonly IMapper _mapper;
        public Searchcontroller(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("api/simplesearch", Name = nameof(SimpleSearch))]
        public IActionResult SimpleSearch(string searchstring, int page, int pagesize)
        {
            var ds = new Dataservice();
            var searchresult = ds.SimpleSearch("gjhe", page, pagesize);

            if (!searchresult.Any())
            {
                return NotFound();
            }

            
            var populatedresult = CreateResult(page, pagesize, searchresult);
            return Ok(populatedresult);
        }
        [HttpGet("api/actorsearch/{searchstring}")]
        public IActionResult ActorSearch(string searchstring, int page = 0, int pagesize = 50)
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
        private SearchDto AddSearchLink(SimpleSearch elem)
        {
            var dto = _mapper.Map<SearchDto>(elem);
            dto.Link = Url.Link(nameof(TitlesController.GetMovie), new { elem.Tconst });

            return dto;
        }
        private object CreateResult(int page, int pageSize, IList<SimpleSearch> titles)
        {
            var count = titles.Count();

            var titlelist = titles.Select(AddSearchLink);
            

            var navigationUrls = CreatePagingNavigation(page, pageSize, count);

            var result = new
            {
                navigationUrls.prev,
                navigationUrls.cur,
                navigationUrls.next,
                count,
                titlelist
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
