using DataserviceLib;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using raww.Models;
using AutoMapper;

namespace raww.Controllers
{
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        public HistoryController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("api/searchhistory/{username}", Name = nameof(SearchHistory))]
        public IActionResult SearchHistory(string username, int page = 0, int pagesize = 10)
        {
            var ds = new Dataservice();
            var searchresult = ds.GetSearchHistory(username, page, pagesize);

            if (!searchresult.Any())
            {
                return NotFound();
            }

            var populatedresult = CreateSearchHistoryResult(username, page, pagesize, searchresult);
            
            return Ok(populatedresult);
        }
        [HttpGet("api/ratinghistory/{username}", Name = nameof(RatingHistory))]
        public IActionResult RatingHistory(string username, int page = 0, int pagesize = 50)
        {
            var ds = new Dataservice();
            var searchresult = ds.GetRatingHistory(username, page, pagesize);

            if (!searchresult.Any())
            {
                return NotFound();
            }

            var populatedresult = CreateRatingHistoryResult(username, page, pagesize, searchresult);
            return Ok(populatedresult);
        }
        private SearchHistoryDto MapSearchElement(Searchhistory elem)
        {
            var dto = _mapper.Map<SearchHistoryDto>(elem);
            //dto.Url = Url.Link(nameof(), new { elem.Nconst });

            return dto;
        }
        private object CreateSearchHistoryResult(string username ,int page, int pageSize, IList<Searchhistory> histories)
        {
            var ds = new Dataservice();

            var count = ds.numberOfSearchHistories(username);

            var mappedhistory = histories.Select(MapSearchElement);

            var navigationUrls = CreatePagingNavigation(page, pageSize, count, nameof(SearchHistory));
            
            var result = new
            {
                navigationUrls.prev,
                navigationUrls.cur,
                navigationUrls.next,
                count,
                mappedhistory
            };

            return result;
        }
        private RatingHistoryDto MapRatingElement(Ratinghistory elem)
        {
            var dto = _mapper.Map<RatingHistoryDto>(elem);
            elem.Tconst = elem.Tconst.Trim();
            dto.Link = Url.Link(nameof(TitlesController.GetMovie), new { elem.Tconst });

            return dto;
        }
        private object CreateRatingHistoryResult(string username, int page, int pageSize, IList<Ratinghistory> histories)
        {
            var ds = new Dataservice();

            var count = ds.numberOfRatingHistories(username);

            var mappedhistory = histories.Select(MapRatingElement);

            var navigationUrls = CreatePagingNavigation(page, pageSize, count, nameof(RatingHistory));

            var result = new
            {
                navigationUrls.prev,
                navigationUrls.cur,
                navigationUrls.next,
                count,
                mappedhistory
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

            if (page < (int) Math.Ceiling((double) count / pageSize) - 1)
            {
                next = Url.Link(prefix, new {page = page + 1, pageSize});
            }

            var cur = Url.Link(prefix, new { page, pageSize });

            return (prev, cur, next);
        }
    }
}
