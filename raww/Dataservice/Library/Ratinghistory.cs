using System;
using System.Collections.Generic;
using System.Text;

namespace DataserviceLib
{
    public class Ratinghistory
    {
        public string Username { get; set; }
        public int Rating { get; set; }
        public string Title { get; set; }
        public string Tconst { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Tconst")]
        public Titlebasics Titlebasics { get; set; }
    }
}
