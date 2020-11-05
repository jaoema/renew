using System;
using System.Collections.Generic;
using System.Text;

namespace DataserviceLib
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Bookmark Bookmark { get; set; }
    }
}
