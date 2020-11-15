using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataserviceLib;
using SqlFunctions;
using raww.Models;

namespace raww.Controllers
{
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMapper _mapper;
        public PersonController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpGet("api/person/{nconst}", Name = nameof(GetPerson))]
        public IActionResult GetPerson(string nconst)
        {
            var ds = new Dataservice();
            var result = ds.GetPerson(nconst);

            if (result == null)
            {
                return NotFound();
            }

            var mapped = _mapper.Map<PersonDto>(result);
            result.Nconst = result.Nconst.Trim(); 
            mapped.Link = Url.Link(nameof(BookmarkController.Bookmark), new { result.Nconst, movie = false });

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
