using System;
using System.Linq;
using System.Collections.Generic;

namespace DataserviceLib
{
    public class Dataservice
    {
        private List<Titlebasics> _titlebasics = new List<Titlebasics>
        {
            new Titlebasics {Tconst = "tconst123", Name = "minfilm"},
            new Titlebasics {Tconst = "tconst1", Name = "minfil"}
            //get data 
        };

        public IList<Titlebasics> GetTitles()
        {
            return _titlebasics;
        }
        public Titlebasics GetTitle(string tconst)
        {
            return _titlebasics.FirstOrDefault(x => x.Tconst == tconst);
        }
    }
}