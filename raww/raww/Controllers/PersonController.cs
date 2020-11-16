using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataserviceLib;
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
            mapped.Link = Url.Link(nameof(BookmarkController.Bookmark), new { id = result.Nconst, movie = false });

            return Ok(mapped);
        }

        [HttpGet("api/popularactors", Name = nameof(PopularActors))]
        public IActionResult PopularActors(int amount, int page, int pagesize)
        {
            var ds = new Dataservice();
            var result = ds.GetPopularActors(amount, page, pagesize);

            if (!result.Any())
            {
                return NotFound();
            }

            var populatedresult = CreatePopularActorResult(page, pagesize, result);

            return Ok(populatedresult);
        }
        private PopularActorDto AddActorLink(Person elem)
        {
            var dto = _mapper.Map<PopularActorDto>(elem);

            elem.Nconst = elem.Nconst.Trim();
            dto.Link = Url.Link(nameof(PersonController.GetPerson), new { elem.Nconst });

            return dto;
        }
        private object CreatePopularActorResult(int page, int pageSize, IList<Person> actors)
        {
            var count = actors.Count();

            var actorlist = actors.Select(AddActorLink);

            var navigationUrls = CreatePagingNavigation(page, pageSize, count, nameof(PopularActors));

            var result = new
            {
                navigationUrls.prev,
                navigationUrls.cur,
                navigationUrls.next,
                count,
                actorlist
            };

            return result;
        }
        private (string prev, string cur, string next) CreatePagingNavigation(int page, int pageSize, int count, string prefix)
        {
            string prev = null;

            if (page > 0)
            {
                prev = Url.Link(prefix, new { page = page - 1, pageSize });
            }

            string next = null;

            if (page < (int)Math.Ceiling((double)count / pageSize) - 1)
                next = Url.Link(prefix, new { page = page + 1, pageSize });

            var cur = Url.Link(prefix, new { page, pageSize });

            return (prev, cur, next);
        }
    }
}
