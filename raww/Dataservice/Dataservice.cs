using System;
using System.Linq;
using System.Collections.Generic;

namespace DataserviceLib
{
    public class Dataservice
    {
        private List<Titlebasics> _titlebasics = new List<Titlebasics>
        {
            new Titlebasics {Tconst = "tconst123", Primarytitle = "minfilm"},
            new Titlebasics {Tconst = "tconst1", Primarytitle = "minfil"}
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

        public Titlebasics SimpleSearch(string searchstring)
        {
            //get results from DB function
            return null;
        }

        public Person FindActor(string searchstring)
        {
            //get results from DB name search function
            return null;
        }

        public Person FindCoActor(string searchstring)
        {
            //get results from db coplayer search function
            return null;
        }
    }
}