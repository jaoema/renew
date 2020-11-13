using System;
using System.Linq;
using System.Collections.Generic;
using System.Dynamic;

namespace DataserviceLib
{
    public class Dataservice
    {
        private List<Titlebasics> _titlebasics = new List<Titlebasics>
        {
            new Titlebasics {Tconst = "tconst123", Titletype = "test", Primarytitle = "minfilm", Originaltitle = "minflm", Isadult = false, Startyear = 2000, Endyear = 2002, Runtimeminutes = 120},
            new Titlebasics {Tconst = "tconst1", Primarytitle = "minfil"}
            //get data 
        };

        private List<SimpleSearch> _SimpleSearch = new List<SimpleSearch>
        {
            new SimpleSearch {Tconst = "tconst123", Title = "test", Year = 2000, Rating = 2.5},
            new SimpleSearch {Tconst = "tconst1", Title = "test2", Year = 200, Rating = 3.5},
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

        public List<Titlebasics> GetSimilarTitles(string id)
        {
            var titles = _titlebasics;
            return titles;
        }
        public void CreateUser(User user)
        {
            //DB create user call
        }

        public bool Login(string username, string password)
        {
            //DB login command
            return true;
        }

        public bool Logout(string username)
        {
            //logout
            return true;
        }

        public IList<SimpleSearch> SimpleSearch(string searchstring, int page = 1, int pagesize = 50)
        {
            //get results from DB function. Take amount equal to page*pagesize -> tolist
            var mylist = _SimpleSearch;
            return mylist;
        }

        public IList<Person> FindActor(string searchstring, int page = 1, int pagesize = 50)
        {
            //get results from DB name search function
            var mylist = new List<Person> { new Person { Nconst = "ncon123", Primaryname = "Mads Mikkelsen" }, new Person { Nconst = "ncon1234", Primaryname = "Peter Mikkelsen" } };
            return mylist;
        }

        public Person GetPerson(string nconst)
        {
            //get person
            return new Person();
        }

        public IList<Person> FindCoActor(string searchstring)
        {
            //get results from db coplayer search function
            var mylist = new List<Person> { new Person { Nconst = "ncon123", Primaryname = "Mads Mikkelsen" }, new Person { Nconst = "ncon1234", Primaryname = "Peter Mikkelsen" } };
            return mylist;
        }

        public Searchhistory GetSearchHistory()
        {
            return null;
        }

        public Ratinghistory GetRatingHistory()
        {
            return null;
        }

        public bool CreateBookmark(string id)
        {
            return true;
        }

        public bool DeleteBookmark(string id)
        {
            return true;
        }

        public Bookmark GetBookmarked()
        {
            Bookmark marked = new Bookmark();
            return marked;
        }

    }
}