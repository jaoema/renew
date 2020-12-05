using System;
using System.Collections.Generic;
using System.Text;

namespace DataserviceLib
{
    public class Titleprincipal
    {
        public string Tconst { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Tconst")]
        public int Ordering { get; set; }
        public string Nconst { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Nconst")]
        public string Category { get; set; }
        public string Job { get; set; }
        public string Characters { get; set; }

        public Titlebasics Titlebasics { get; set; }
    }
}
