using System;
using System.Linq;
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using SqlFunctions;
using Npgsql;

namespace DataserviceLib
{
    public class Dataservice
    {

        string connectionString = "host=localhost;db=imdb;uid=postgres;pwd =Franet0365";
        // string connectionString = "host=localhost;db=imdb;uid=postgres;pwd =Baad666";

        string adminUsername = "hans1";
        string adminPassword = "grethe";


        private List<Titlebasics> _titlebasics = new List<Titlebasics>
        {
            new Titlebasics {Tconst = "tconst123", Titletype = "test", Primarytitle = "minfilm", Originaltitle = "minflm", Isadult = false, Startyear = 2000, Endyear = 2002, Runtimeminutes = 120},
            new Titlebasics {Tconst = "tconst1", Primarytitle = "minfil"}
            //get data 
        };

        /*private List<SimpleSearch> _SimpleSearch = new List<SimpleSearch>
        {
            new SimpleSearch {Tconst = "tconst123", Title = "test", Year = 2000, Rating = 2.5},
            new SimpleSearch {Tconst = "tconst1", Title = "test2", Year = 200, Rating = 3.5},
            //get data 
        };*/


        public Titlebasics GetTitle(string tconst)
        {
            var ctx = new ImdbContext(connectionString);
            var title = ctx.Titlebasicses.Find(tconst);

            return title;
        }

        public bool CreateUser(string username, string password)
        {
            var ctx = new ImdbContext(connectionString);

            /*var user = ctx.Users.Find(username);

            if (user == null)
            {
                ctx.Database.ExecuteSqlInterpolated($"select create_user({username}, {password})");
                ctx.SaveChanges();
                return true;
            }
            else
            {
                return false;
            } */

            var result = ctx.Database.ExecuteSqlInterpolated($"select create_user({username}, {password})");
            ctx.SaveChanges();
            return true;

            /* var ctx = new ImdbContext(connectionString);

                 var connection = (NpgsqlConnection)ctx.Database.GetDbConnection();
                 connection.Open();
                 var cmd = new NpgsqlCommand($"select create_user({username},{password})", connection);
                 cmd.ExecuteNonQuery();
                 return true; */

        }

        public bool Login(string username, string password)
        {
            //DB login command
            var ctx = new ImdbContext(connectionString);

            //var user = ctx.Users.Find(username);

            if (username == adminUsername && password == adminPassword)
            {
                return true;

            }

            return false;

        }

        public bool Logout(string username)
        {
            //logout
            return true;
        }

        public IList<SimpleSearch> SimpleSearch(string searchstring, int page = 0, int pagesize = 50)
        {
            //get results from DB function. Take amount equal to page*pagesize -> tolist
            var mylist = new List<SimpleSearch>();
            // use string search
            var ctx = new ImdbContext(connectionString);
            var result = ctx.SimpleSearches.FromSqlInterpolated($"select * from string_search({adminUsername},{searchstring})");

            foreach (var searchResult in result)
            {
                mylist.Add(searchResult);
            }

            return mylist
                .Skip(page * pagesize)
                .Take(pagesize)
                .ToList();
        }




        public IList<Person> FindActor(string searchstring, int page = 0, int pagesize = 50)
        {
            //get results from DB name search function
            var mylist = new List<Person>();

            var ctx = new ImdbContext(connectionString);
            var result = ctx.Persons.FromSqlInterpolated($"select * from name_search({adminUsername},{searchstring})");

            foreach (var searchResult in result)
            {
                mylist.Add(searchResult);
            }

            return mylist;
        }

        public Person GetPerson(string nconst)
        {
            //get person
            var ctx = new ImdbContext(connectionString);
            var person = ctx.Persons.Find(nconst);

            return person;

        }

        public IList<Person> FindCoActor(string searchstring)
        {
            //get results from db coplayer search function
            var mylist = new List<Person> { new Person { Nconst = "ncon123", Primaryname = "Mads Mikkelsen" }, new Person { Nconst = "ncon1234", Primaryname = "Peter Mikkelsen" } };
            return mylist;
        }

        public IList<Person> GetPopularActors(int amount, int page = 0, int pagesize = 50)
        {
            var mylist = new List<Person> { new Person { Nconst = "ncon123", Primaryname = "Mads Mikkelsen" }, new Person { Nconst = "ncon1234", Primaryname = "Peter Mikkelsen" } };
            return mylist
                .Skip(page * pagesize)
                .Take(pagesize)
                .ToList();
        }
        public IList<Searchhistory> GetSearchHistory(int page = 0, int pagesize = 50)
        {
            var mylist = new List<Searchhistory>();
            var ctx = new ImdbContext(connectionString);

            var result = ctx.Searchhistories.FromSqlInterpolated($"select * from searchhistory where username = {adminUsername}");

            foreach (var searchResult in result)
            {
                mylist.Add(searchResult);
            }

            return mylist
                .Skip(page * pagesize)
                .Take(pagesize)
                .ToList();
        }

        public IList<Ratinghistory> GetRatingHistory(int page = 0, int pagesize = 50)
        {
            var mylist = new List<Ratinghistory>();
            var ctx = new ImdbContext(connectionString);

            var result = ctx.Ratinghistories.FromSqlInterpolated($"select * from ratinghistory where username = {adminUsername}");

            foreach (var searchResult in result)
            {
                mylist.Add(searchResult);
            }

            return mylist
                .Skip(page * pagesize)
                .Take(pagesize)
                .ToList();
        }

        public bool CreateBookmark(string id, bool movie)
        {
            var ctx = new ImdbContext(connectionString);

            string type;
            if (movie)
            {
                type = "movie";
            }
            else
            {
                type = null;
            }

            ctx.Database.ExecuteSqlInterpolated($"select bookmark({adminUsername},{id}, {type})");
            ctx.SaveChanges();

            //var bookmark = ctx.Bookmarks.Find(adminUsername, id);

            //if (bookmark == null)
            //{
            //    return false;
            //}

            return true;

        }

        public bool DeleteBookmark(string id)
        {
            return true;
        }

        public IList<Bookmark> GetBookmarked(int page = 1, int pagesize = 50)
        {

            var mylist = new List<Bookmark>();
            var ctx = new ImdbContext(connectionString);

            var result = ctx.Bookmarks.FromSqlInterpolated($"select * from bookmarked where username = {adminUsername}");

            foreach (var searchResult in result)
            {
                mylist.Add(searchResult);
            }

            return mylist
                .Skip(page * pagesize)
                .Take(pagesize)
                .ToList();
        }

    }
}