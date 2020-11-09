using DataserviceLib;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace raww.Controllers
{
    [ApiController]
    public class BookmarkController : ControllerBase
    {
        [HttpGet("api/bookmark/{id}")]
        public IActionResult Bookmark(string id)
        {
            var ds = new Dataservice();
            bool bookmark = ds.CreateBookmark(id);

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
        [HttpGet("api/bookmarked")]
        public IActionResult Bookmarked()
        {
            var ds = new Dataservice();
            var searchresult = ds.GetBookmarked();

            if (searchresult == null)
            {
                return NotFound();
            }

            return Ok(searchresult);
        }
    }
}