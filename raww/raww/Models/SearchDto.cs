using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace raww.Models
{
    public class SearchDto
    {
        public string Tconst { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rating { get; set; }
    }
}
