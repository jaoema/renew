using DataserviceLib;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace raww.Controllers
{
    [ApiController]
    public class Usercontroller : ControllerBase
    {
        [HttpPost("api/signup?username={username}&password={password}")]
        public IActionResult CreateUser(User user)
        {
            var ds = new Dataservice();
            ds.CreateUser(user);

            return Created("",user.Username);
        }

        [HttpPost("api/login?username={username}&password={password}")]
        public IActionResult Login(User user)
        {
            var ds = new Dataservice();
            ds.Logout(user.Username);

            return Ok();
        }

        [HttpPost("api/logout?username={username}")]
        public IActionResult Logout(User user)
        {
            var ds = new Dataservice();
            ds.CreateUser(user);

            return Created("", user.Username);
        }
    }
}
