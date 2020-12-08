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
        
        [HttpGet("api/simplesearch/{searchstring}", Name = nameof(SimpleSearch))]
        public IActionResult SimpleSearch(string searchstring, int page = 0, int pagesize = 50)
        {
            var ds = new Dataservice();
            var searchresult = ds.SimpleSearch(searchstring, page, pagesize);

            if (!searchresult.Item1.Any())
            {
                return NotFound();
            }

            var populatedresult = CreateSimpleSearchResult(page, pagesize, searchresult.Item1, searchresult.Item2);
            return Ok(populatedresult);
        }
        [HttpGet("api/namesearch/{searchstring}", Name = nameof(NameSearch))]
        public IActionResult NameSearch(string searchstring, int page = 0, int pagesize = 50)
        { 
            var ds = new Dataservice();
            var searchresult = ds.FindActors(searchstring, page, pagesize);

            if (!searchresult.Item1.Any())
            {
                return NotFound();
            }
            
            var populatedresult = CreateNameSearchResult(page, pagesize, searchresult.Item1, searchresult.Item2);
            return Ok(populatedresult);
        }
        [HttpGet("api/coactorsearch/{searchstring}", Name = nameof(CoActorSearch))]
        public IActionResult CoActorSearch(string searchstring, int page, int pagesize)
        {
            var ds = new Dataservice();
            var searchresult = ds.FindCoActor(searchstring);

            if (searchresult == null)
            {
                return NotFound();
            }

            var populatedresult = CreateCoActorSearchResult(page, pagesize, searchresult);

            return Ok(populatedresult);
        }
        private SearchDto AddSearchLink(SimpleSearch elem)
        {
            var dto = _mapper.Map<SearchDto>(elem);
            elem.Tconst = elem.Tconst.Trim();
            dto.Link = Url.Link(nameof(TitlesController.GetMovie), new { elem.Tconst });

            return dto;
        }
        private PersonDto AddNameLink(Person elem)
        {
            var dto = _mapper.Map<PersonDto>(elem);

            elem.Nconst = elem.Nconst.Trim();
            dto.Link = Url.Link(nameof(PersonController.GetPerson), new { elem.Nconst });

            return dto;
        }
        private CoActorDto AddCoActorLink(Person elem)
        {
            var dto = _mapper.Map<CoActorDto>(elem);
            elem.Nconst = elem.Nconst.Trim();
            dto.Link = Url.Link(nameof(PersonController.GetPerson), new { elem.Nconst });

            return dto;
        }

       
        private object CreateSimpleSearchResult(int page, int pageSize, IList<SimpleSearch> titles, int count)
        {
            var titlelist = titles.Select(AddSearchLink);
            
            var navigationUrls = CreatePagingNavigation(page, pageSize, count, nameof(SimpleSearch));

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
        private object CreateNameSearchResult(int page, int pageSize, IList<Person> persons, int count)
        {
            var personlist = persons.Select(AddNameLink);

            var navigationUrls = CreatePagingNavigation(page, pageSize, count, nameof(NameSearch));

            var result = new
            {
                navigationUrls.prev,
                navigationUrls.cur,
                navigationUrls.next,
                count,
                personlist
            };

            return result;
        }
        private object CreateCoActorSearchResult(int page, int pageSize, IList<Person> persons)
        {
            var count = persons.Count();

            var personlist = persons.Select(AddCoActorLink);

            var navigationUrls = CreatePagingNavigation(page, pageSize, count, nameof(CoActorSearch));

            var result = new
            {
                navigationUrls.prev,
                navigationUrls.cur,
                navigationUrls.next,
                count,
                personlist
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
