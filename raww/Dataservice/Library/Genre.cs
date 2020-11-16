using System;
using System.Collections.Generic;
using System.Text;

namespace DataserviceLib
{
    public class Genre
    {
        public string Tconst { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Tconst")]
        public string Name { get; set; }
    }
}
