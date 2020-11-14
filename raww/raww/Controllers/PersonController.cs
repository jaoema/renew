using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataserviceLib;
using SqlFunctions;
using raww.Models;

namespace raww.Controllers
{
    [ApiController]
    public class PersonController : ControllerBase
    {
        AutoMapper.IMapper mapper;
        [HttpGet("api/person/{nconst}", Name = nameof(GetPerson))]
        public IActionResult GetPerson(string nconst)
        {
            var ds = new Dataservice();
            var result = ds.FindActor(nconst);

            if (result == null)
            {
                return NotFound();
            }

            var mapped = mapper.Map<PersonDto>(result);
            mapped.Link = Url.Link(nameof(BookmarkController.Bookmark), new { mapped.Nconst, movie = false });

            return Ok(mapped);
        }
        /*
        [HttpGet("api/{search}", Name = nameof(SearchName))]
        public IActionResult SearchName(string search)
        {
            var ds = new Dataservice();
            var result = ds.SearchName(search);

            return Ok(result);
        }*/

        [HttpGet("api/popularactors", Name = nameof(PopularActors))]
        public IActionResult PopularActors(int amount, int page, int pagesize)
        {
            var ds = new Dataservice();
            var result = ds.GetPopularActors(amount, page, pagesize);

            return Ok(result);
        }
    }
}
