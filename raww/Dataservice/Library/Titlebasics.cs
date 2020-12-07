using System;
using System.Collections.Generic;
using System.Text;

namespace DataserviceLib
{
    public class Titlebasics
    {
        public string Tconst { get; set; }
       

        public string Titletype { get; set; }
        public string Primarytitle { get; set; }
        public string Originaltitle { get; set; }
        public bool Isadult { get; set; }
        public string Startyear { get; set; }
        public string Endyear { get; set; }
        //public string TitleprincipalTconst { get; set; }
        public List<Titleprincipal> Titleprincipal {get; set;}

       



        /**
         * Due to the database containing null values, we needed to remove this Runtimeminutes
         * otherwise it would crash our application when running, this needs to be fixed later.
         * public int Runtimeminutes { get; set; }
        **/
    }
}
