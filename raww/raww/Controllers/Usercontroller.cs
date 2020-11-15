using DataserviceLib;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace raww.Controllers
{
    [ApiController]
    public class Usercontroller : ControllerBase
    {
        [HttpPost("api/signup")] //?username={username}&password={password}
        public IActionResult CreateUser(string username, string password)
        {
            var ds = new Dataservice();
            var created = ds.CreateUser(username, password);

            if (!created)
            {
                return Conflict();
            }
            return Ok();
        }

        [HttpPost("api/login")]
        public IActionResult Login(string username, string password)
        {
            var ds = new Dataservice();
            var success = ds.Login(username, password);

            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPost("api/logout")] //?username={username}
        public IActionResult Logout(string username)
        {
            var ds = new Dataservice();
            ds.Logout(username);

            return Ok();
        }
    }
}