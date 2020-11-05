using System;
using System.Collections.Generic;
using System.Text;

namespace DataserviceLib
{
    public class Ratinghistory
    {
        public string Username { get; set; }
        public int Rating { get; set; }
        public string Tconst { get; set; }
        public Titlebasics Titlebasics { get; set; }
    }
}
