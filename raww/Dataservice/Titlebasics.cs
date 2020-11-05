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
        public int Startyear { get; set; }
        public int Endyear { get; set; }
        public int Runtimeminutes { get; set; }
        public Titleepisode Titleepisode { get; set; }
        public Titleaka Titleakas { get; set; }
        public Titleprincipal Titleprincipals { get; set; }
        public Titlerating Titleratings { get; set; }
        public Omdb Omdb { get; set; }
        public Genre Genre { get; set; }
    }
}
