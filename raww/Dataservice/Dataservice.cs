using System;
using System.Linq;
using System.Collections.Generic;

namespace DataserviceLib
{
    public class Dataservice
    {
        private List<Movie> _movies = new List<Movie>
        {
            new Movie {Tconst = "tconst123", Name = "minfilm"},
            new Movie {Tconst = "tconst1", Name = "minfil"}
            //get data 
        };

        public IList<Movie> GetMovies()
        {
            return _movies;
        }
        public Movie GetMovie(string tconst)
        {
            return _movies.FirstOrDefault(x => x.Tconst == tconst);
        }
    }
}
