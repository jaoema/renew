using System;
using System.Linq;
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DataserviceLib
{
    public class Dataservice : IDataService
    {

        string connectionString = "host=localhost;db=imdb;uid=postgres;pwd =Franet0365";
        //string connectionString = "host=localhost;db=imdb;uid=postgres;pwd =Baad666";
        string adminUsername = "hans1";
        string adminPassword = "grethe";



        public Titlebasics GetTitle(string tconst)
        {
            var ctx = new ImdbContext(connectionString);
            var title = ctx.Titlebasicses.Find(tconst);
            return title;
        }

        public bool CreateUser(string username, string password)
        {
            var ctx = new ImdbContext(connectionString);
            var user = ctx.Users.Find(username);

            if (user == null)
            {
                ctx.Database.ExecuteSqlInterpolated($"select create_user({username}, {password})");
                ctx.SaveChanges();
                return true;
            }
            else
            {
                return false;
            } 
        }

        public bool Login(string username, string password)
        {
            //DB login command
            var ctx = new ImdbContext(connectionString);
          
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
            return mylist
            .Skip(page * pagesize)
            .Take(pagesize)
            .ToList();
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
            var ctx = new ImdbContext(connectionString);
            var result = ctx.Searchhistories
                .Where(x => x.Username == adminUsername)
                .Skip(page * pagesize)
                .Take(pagesize)
                .ToList();

                return result;

        }

        public IList<Ratinghistory> GetRatingHistory(int page = 0, int pagesize = 50)
        {
            
            var ctx = new ImdbContext(connectionString);
            var result = ctx.Ratinghistories
                .Where(x => x.Username == adminUsername)
                .Skip(page * pagesize)
                .Take(pagesize)
                .ToList();

            return result;
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
            return true;
        }

        public bool DeleteBookmark(string id)
        {
            return true;
        }

        public IList<Bookmark> GetBookmarked(int page = 0, int pagesize = 50)
        {
            var ctx = new ImdbContext(connectionString);
            var result = ctx.Bookmarks
                .Where(x => x.Username == adminUsername)
                .Skip(page * pagesize)
                .Take(pagesize)
                .ToList();

            return result;

        }
        public bool Rate(string tconst, int rating)
        {
            var ctx = new ImdbContext(connectionString);
            var result = ctx.Database.ExecuteSqlInterpolated($"select rate({adminUsername}, {tconst},{rating})");
            ctx.SaveChanges();
            return true;
        }


  
    }
}