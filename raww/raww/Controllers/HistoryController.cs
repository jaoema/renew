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
    }
}
