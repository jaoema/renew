using System;
using System.Linq;
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DataserviceLib
{
    public class Dataservice
    {

        // Hej Rasmus - nyt sted at skifte connectionstring inde i "ImdbContext"
        string adminUsername = "hans1";
        string adminPassword = "grethe";



        public Titlebasics GetTitle(string tconst)
        {
            var ctx = new ImdbContext();
            var title = ctx.Titlebasicses.Find(tconst);
            return title;
        }

        public bool CreateUser(string username, string password)
        {
            var ctx = new ImdbContext();
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
            var ctx = new ImdbContext();

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
            var ctx = new ImdbContext();
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

            var ctx = new ImdbContext();

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
            var ctx = new ImdbContext();
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
        public Object GetSpecificMovie(string tconst)
        {
            var ctx = new ImdbContext();

            // from p in ctx.Titlebasicses
            // select new { Tconst = p.Tconst, Primaryname = p.Primarytitle, p.Titleprincipal.Characters, };

            var dataa = ctx.Titlebasicses.Find(tconst);
                //.Where(x => x.Tconst == tconst)
               // .Include(ctx.Titleprincipals.Tconst);

           // ctx.Entry(dataa).Collection("Titleprincipals").Load();

            /*dataa.titleprincipal = ctx.Titleprincipals
                .Where(x => x.Tconst == tconst)
                .First<Titleprincipal>();
                
            
            /*Join(
                ctx.Titleprincipals,
            titlebasics => titlebasics.Tconst,
            titleprincipal => titleprincipal.Titlebasics.Tconst,
            (titlebasics, titleprincipal) => new
            {
                Tconst = titleprincipal.Tconst,
                Primarytitle = titlebasics.Primarytitle,
                Nconst = titleprincipal.Nconst,

                //BookTitle = book.Title
            }
        );   */
   
            return dataa;
           
        } 

        

/*public IList<object> GetSpecificPerson(string nconst)
        {
            var ctx = new ImdbContext();
            var data = ctx.Persons.Where(x => x.
        .Join(
            ctx.Titlebasicses,
            Person => Person.Nconst,
            Titlebasics => Titlebasics.Person.AuthorId,
            (author, book) => new
            {
                BookId = book.BookId,
                AuthorName = author.Name,
                BookTitle = book.Title
            }
        ).ToList();
	
    foreach(var book in data)
    {
        Console.WriteLine("Book Title: {0} \n\t Written by {1}", book.BookTitle, book.AuthorName);
    }
    }*/

        public IList<Searchhistory> GetSearchHistory(int page = 0, int pagesize = 50)
        {
            using var ctx = new ImdbContext();

            var result = ctx.Searchhistories
                .Where(x => x.Username == adminUsername);

            return result
                    .Skip(page * pagesize)
                    .Take(pagesize)
                    .ToList();
        }

        public int numberOfSearchHistories()
        {
            using var ctx = new ImdbContext();
            return ctx.Searchhistories
                .Count(x => x.Username == adminUsername);
        }

        public IList<Ratinghistory> GetRatingHistory(int page = 0, int pagesize = 50)
        {
            var ctx = new ImdbContext();
            var result = ctx.Ratinghistories
                .Where(x => x.Username == adminUsername)
                .Skip(page * pagesize)
                .Take(pagesize)
                .ToList();

            return result;
        }

        public int numberOfRatingHistories()
        {
            using var ctx = new ImdbContext();
            return ctx.Searchhistories
                .Count(x => x.Username == adminUsername);
        }

        public bool CreateBookmark(string id, bool movie)
        {
            var ctx = new ImdbContext();
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
            var ctx = new ImdbContext();
            var result = ctx.Bookmarks
                .Where(x => x.Username == adminUsername)
                .Skip(page * pagesize)
                .Take(pagesize)
                .ToList();

            return result;
        }
        public int numberOfBookmarks()
        {
            using var ctx = new ImdbContext();
            return ctx.Bookmarks
                .Count(x => x.Username == adminUsername);
        }
        public bool Rate(string tconst, int rating)
        {
            var ctx = new ImdbContext();
            var result = ctx.Database.ExecuteSqlInterpolated($"select rate({adminUsername}, {tconst},{rating})");
            ctx.SaveChanges();
            return true;
        }



    }
}