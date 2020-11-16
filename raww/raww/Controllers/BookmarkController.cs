using AutoMapper;
using DataserviceLib;
using Microsoft.AspNetCore.Mvc;
using raww.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace raww.Controllers
{
    [ApiController]
    public class BookmarkController : ControllerBase
    {
        private readonly IMapper _mapper;
        public BookmarkController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpPost("api/bookmark/{id}", Name = nameof(Bookmark))]
        public IActionResult Bookmark(string id, bool movie)
        {
            var ds = new Dataservice();
            bool bookmark = ds.CreateBookmark(id, movie);

            if (!bookmark)
            {
                return NotFound();
            }

            return Ok();
        }
        [HttpDelete("api/bookmark/{id}")]
        public IActionResult RemoveBookmark(string id)
        {
            var ds = new Dataservice();
            bool done = ds.DeleteBookmark(id);

            if (!done)
            {
                return NotFound();
            }

            return Ok();
        }
        [HttpGet("api/bookmarked", Name = nameof(Bookmarked))]
        public IActionResult Bookmarked(int page = 0, int pagesize = 50)
        {
            var ds = new Dataservice();
            var searchresult = ds.GetBookmarked(page, pagesize);

            if (searchresult == null)
            {
                return NotFound();
            }

            var populatedresult = CreateBookmarkedResult(searchresult, page, pagesize);

            return Ok(searchresult);
        }
        private BookmarkDto MapBookmarks(Bookmark elem)
        {
            var dto = _mapper.Map<BookmarkDto>(elem);

            if (elem.Nconst != null)
            {
                elem.Nconst = elem.Nconst.Trim();
                dto.Link = Url.Link(nameof(PersonController.GetPerson), new { elem.Nconst });
            } else
            {
                elem.Tconst = elem.Tconst.Trim();
                dto.Link = Url.Link(nameof(TitlesController.GetMovie), new { elem.Tconst });
            }

            return dto;
        }
        private object CreateBookmarkedResult(IList<Bookmark> bookmarks, int page = 1, int pageSize = 50)
        {
            var count = bookmarks.Count();

            var personlist = bookmarks.Select(MapBookmarks);

            var navigationUrls = CreatePagingNavigation(page, pageSize, count, nameof(Bookmarked));

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