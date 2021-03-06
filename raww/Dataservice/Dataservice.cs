﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DataserviceLib
{
    public class Dataservice
    {
        public Titlebasics GetTitle(string tconst)
        {
           //definere vores imdbcontext
            using var ctx = new ImdbContext();
            //finder tconst for vores entity titlbasics. 
           return ctx.Titlebasicses.Find(tconst);
        }

        public bool CreateUser(string username, string password)
        {
            using var ctx = new ImdbContext();
            //tjekker om der findes et username i databasen
            var user = ctx.Users.Find(username);
            //hvis user ikke findes, laves der en user ved at kalde en sql query
            if (user == null)
            {
                ctx.Database.ExecuteSqlInterpolated($"select create_user({username}, {password})");
                //Gemmer ændringer, så de kommer ind i databasen. 
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
            using var ctx = new ImdbContext();

            var data = ctx.Users.Find(username);

            if(data != null)
            {
                if (username == data.Username && password == data.Password)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Logout(string username)
        {
            //logout
            return true;
        }

        public (IList<SimpleSearch>, int amount) SimpleSearch(string username, string searchstring, int page = 0, int pagesize = 50)
        {
            //get results from DB function. Take amount equal to page*pagesize -> tolist
            var mylist = new List<SimpleSearch>();
            // use string search
            var ctx = new ImdbContext();
            var result = ctx.SimpleSearches.FromSqlInterpolated($"select * from string_search({username},{searchstring})");

            foreach (var searchResult in result)
            {
                mylist.Add(searchResult);
            }

            var amount = mylist.Count();
            var pagedlist = mylist
                .Skip(page * pagesize)
                .Take(pagesize)
                .ToList();

            return (pagedlist, amount);
        }

        public (IList<Person>, int amount) FindActors(string username, string searchstring, int page = 0, int pagesize = 50)
        {
            //get results from DB name search function
            var mylist = new List<Person>();

            using var ctx = new ImdbContext();

            var result = ctx.Persons.FromSqlInterpolated($"select * from name_search({username},{searchstring})");


            foreach (var searchResult in result)
            {
                mylist.Add(searchResult);
            }
            var amount = mylist.Count();
            var pagedlist = mylist
            .Skip(page * pagesize)
            .Take(pagesize)
            .ToList();

            return (pagedlist, amount);
        }

        public Person GetPerson(string nconst)
        {
            //get person
            using var ctx = new ImdbContext();

            return ctx.Persons.Find(nconst);

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
            using var ctx = new ImdbContext();

            return ctx.Titlebasicses
                .Where(x => x.Tconst == tconst)
                //.Include(x => x.Titleprincipal)
                .FirstOrDefault(x => x.Tconst == tconst);
               
               
           
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

        public IList<Searchhistory> GetSearchHistory(string username, int page = 0, int pagesize = 50)
        {
            using var ctx = new ImdbContext();

            var result = ctx.Searchhistories
                .Where(x => x.Username == username);

            return result
                    .Skip(page * pagesize)
                    .Take(pagesize)
                    .ToList();
        }

        public int numberOfSearchHistories(string username)
        {
            using var ctx = new ImdbContext();
            return ctx.Searchhistories
                .Count(x => x.Username == username);
        }

        public IList<Ratinghistory> GetRatingHistory(string username, int page = 0, int pagesize = 50)
        {
            using var ctx = new ImdbContext();
            var result = ctx.Ratinghistories
                .Where(x => x.Username == username)
                .Skip(page * pagesize)
                .Take(pagesize)
                .ToList();

            return result;
        }

        public int numberOfRatingHistories(string username)
        {
            using var ctx = new ImdbContext();
            return ctx.Ratinghistories
                .Count(x => x.Username == username);
        }

        public bool DeleteRating(string username, string id)
        {
            using var ctx = new ImdbContext();

            var rating = ctx.Ratinghistories.Where(x => x.Username == username).Where(x => x.Tconst == id).ToList();

            if (rating.Count != 0)
            {
               
                ctx.Database.ExecuteSqlInterpolated($"select delete_rating({username},{id})");
                ctx.SaveChanges();
                return true;
            }

            return false;
        }

        public bool CreateBookmark(string username, string id, bool movie)
        {
            var ctx = new ImdbContext();


            string type;
            if (movie)
            {
                var bookmark = ctx.Bookmarks
                .Where(x => x.Username == username)
                .Where(x => x.Tconst == id)
                .ToList();

                if(bookmark.Count == 0)
                {
                    type = "movie";
                    ctx.Database.ExecuteSqlInterpolated($"select bookmark({username},{id}, {type})");
                    ctx.SaveChanges();
                    return true;
                }

            }
            else
            {
                var bookmark = ctx.Bookmarks
                .Where(x => x.Username == username)
                .Where(x => x.Nconst == id)
                .ToList();

                if (bookmark.Count == 0)
                {
                    type = "";
                    ctx.Database.ExecuteSqlInterpolated($"select bookmark({username},{id}, {type})");
                    ctx.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public bool DeleteBookmark(string username, string id, bool movie)
        {

            var ctx = new ImdbContext();
            string type;

            if (movie)
            {
                var bookmark = ctx.Bookmarks.Where(x => x.Username == username).Where(x => x.Tconst == id).ToList();

                if(bookmark.Count != 0)
                {
                    type = "movie";
                    ctx.Database.ExecuteSqlInterpolated($"select delete_bookmark({username},{id}, {type})");
                    ctx.SaveChanges();
                    return true;
                }

            }
            else
            {
                var bookmark = ctx.Bookmarks.Where(x => x.Username == username).Where(x => x.Nconst == id).ToList();

                if (bookmark.Count != 0)
                {
                    type = "";
                    ctx.Database.ExecuteSqlInterpolated($"select delete_bookmark({username},{id}, {type})");
                    ctx.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public IList<Bookmark> GetBookmarked(string username, int page = 0, int pagesize = 50)
        {
            var ctx = new ImdbContext();
            var result = ctx.Bookmarks
                .Where(x => x.Username == username)
                .Skip(page * pagesize)
                .Take(pagesize)
                .ToList();

            return result;
        }
        public int numberOfBookmarks(string username)
        {
            using var ctx = new ImdbContext();
            return ctx.Bookmarks
                .Count(x => x.Username == username);
        }

        public bool Rate(string username, string tconst, int rating)
        {
            var ctx = new ImdbContext();

            var ratings = ctx.Ratinghistories
               .Where(x => x.Username == username)
               .Where(x => x.Tconst == tconst)
               .ToList();

            if (ratings.Count == 0)
            {
                var result = ctx.Database.ExecuteSqlInterpolated($"select rate({username}, {tconst},{rating})");
                ctx.SaveChanges();
                return true;
            }

            return false;
        }

    }
}