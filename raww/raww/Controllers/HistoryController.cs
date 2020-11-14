using DataserviceLib;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace raww.Controllers
{
    [ApiController]
    public class HistoryController : ControllerBase
    {
        [HttpGet("api/searchhistory")]
        public IActionResult SearchHistory()
        {
            var ds = new Dataservice();
            var searchresult = ds.GetSearchHistory();

            if (searchresult == null)
            {
                return NotFound();
            }

            return Ok(searchresult);
        }
        [HttpGet("api/ratinghistory")]
        public IActionResult RatingHistory()
        {
            var ds = new Dataservice();
            var searchresult = ds.GetRatingHistory();

            if (searchresult == null)
            {
                return NotFound();
            }

            return Ok(searchresult);
        }
        private CoActorDto AddCoActorLink(Person elem)
        {
            var dto = _mapper.Map<CoActorDto>(elem);
            dto.Link = Url.Link(nameof(PersonController.GetPerson), new { elem.Nconst });

            return dto;
        }
        private object CreateSearchHistoryResult(int page, int pageSize, IList<Person> persons)
        {
            var count = persons.Count();

            var linkedhistory = persons.Select(AddCoActorLink);

            var navigationUrls = CreatePagingNavigation(page, pageSize, count, nameof(CoActorSearch));

            var result = new
            {
                navigationUrls.prev,
                navigationUrls.cur,
                navigationUrls.next,
                count,
                linkedhistory
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
