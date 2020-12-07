using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataserviceLib
{
    public class Titleprincipal
    {
        
        public string Tconst { get; set; }
        //[ForeignKey("Tconst")]

        public int Ordering { get; set; }
        //[System.ComponentModel.DataAnnotations.Schema.ForeignKey("Nconst")]
        public string Nconst { get; set; }
        
        public string Category { get; set; }
        public string Job { get; set; }
        public string Characters { get; set; }

       // [ForeignKey("Titlebasics")]
       // public virtual string Tconst { get; set; }
        public Titlebasics Titlebasics { get; set; }
    }
}
