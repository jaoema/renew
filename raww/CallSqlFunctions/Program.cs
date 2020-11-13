using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;

namespace CallSqlFunctions
{
    class Program
    {
        static void Main(string[] args)
        {
            //var connectionString = "host=localhost;db=imdb;uid=postgres;pwd =Baad666";
            var connectionString = "host=localhost;db=amdb;uid=postgres;pwd =Franet0365";

            //(UseAdo(connectionString);
            //NameSearch(connectionString);
            //FindPopularActors(connectionString);
            AddRatingHistory(connectionString);
            // CreateUser(connectionString);
            //Rate(connectionString);
            //Login(connectionString);
            //NameRating(connectionString);
            //StringSearch(connectionString);
            //Bookmark(connectionString);
        }

        //This method searches for a name, the ref for this method is NS

        public static void NameSearch(string connectionString)
        {

            var usernam = "hans1";
            var searchterm = "Mads";

            var ctx = new ImdbContext(connectionString);
            var result = ctx.Name_Search.FromSqlInterpolated($"select * from name_search({usernam},{searchterm})");

            foreach (var Name_Search in result)
            {
                Console.WriteLine($"{ Name_Search.nconst}, { Name_Search.primaryname}");
            }
        }
        // Finds popular actors the ref for this method is FPA
        private static void FindPopularActors(string connectionString)
        {
            var ctx = new ImdbContext(connectionString);
            var result = ctx.Find_Popular_Actors.FromSqlInterpolated($"select * from popular_actors(10) ");

            foreach (var Find_Popular_Actors in result)
            {
                Console.WriteLine($"{ Find_Popular_Actors.primaryname}, {Find_Popular_Actors.rating}");
            }
        }
       
        private static void AddRatingHistory(string connectionString)
        {
            var usernam = "hans1";
            var tconst = "tt10850402";
            int rating = 9;


            var ctx = new ImdbContext(connectionString);

            var connection = (NpgsqlConnection)ctx.Database.GetDbConnection();
            connection.Open();
            var cmd = new NpgsqlCommand($"select add_rating_history({usernam}, {tconst}, {rating})", connection);
            cmd.ExecuteNonQuery();
        }

        private static void CreateUser(string connectionString)
        {
            var ctx = new ImdbContext(connectionString);
            var connection = (NpgsqlConnection)ctx.Database.GetDbConnection();
            connection.Open();
            var cmd = new NpgsqlCommand($"select create_user('ddheas', 'eerrrghg')", connection);
            cmd.ExecuteNonQuery();
        }

        private static void Rate(string connectionString)
        {
            var ctx = new ImdbContext(connectionString);
            var connection = (NpgsqlConnection)ctx.Database.GetDbConnection();
            connection.Open();
            var cmd = new NpgsqlCommand($"select rate('hans1', 'tt11097072', 2)", connection);
            cmd.ExecuteNonQuery();
        }

        private static void Bookmark(string connectionString)
        {
            var ctx = new ImdbContext(connectionString);
            var connection = (NpgsqlConnection)ctx.Database.GetDbConnection();
            connection.Open();
            var cmd = new NpgsqlCommand($"select bookmark('alex5', 'Mads Mikkelsen', 'Act')", connection);
            cmd.ExecuteNonQuery();

        }

        //Find_Coplayers

        private static void Login(string connectionString)
        {
            var ctx = new ImdbContext(connectionString);
            var connection = (NpgsqlConnection)ctx.Database.GetDbConnection();
            connection.Open();
            var cmd = new NpgsqlCommand($"select login('rasmus2', 'baad')", connection);
            cmd.ExecuteNonQuery();
                        
        }

        
         private static void NameRating(string connectionString)
        {
            var ctx = new ImdbContext(connectionString);
            var result = ctx.Name_Rating.FromSqlInterpolated($"select name_rating('Mads Mikkelsen')");

            foreach (var Name_Rating in result)
            {
                Console.WriteLine($"{ Name_Rating.primaryname}");
            }
        } 

        public static void StringSearch(string connectionString)
        {
            var ctx = new ImdbContext(connectionString);
            var result = ctx.String_Search.FromSqlInterpolated($"select * from string_search('hans1','vampire')");

            foreach (var String_Search in result)
            {
                Console.WriteLine($"{String_Search.tconst}, { String_Search.primarytitle}");
            }
        }

        
        

        private static void UseAdo(string connectionString)
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            //var cmd = new NpgsqlCommand("select * from find_coplayers('%ab%')", connection);
            //var cmd = new NpgsqlCommand("select * from name_search('hans1','Mads')", connection);

            //hans1 needs to be replaced by Username later on. So that it works for everyone.
            var cmd = new NpgsqlCommand("select * from name_search(hans1,%ab%)", connection);

         
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"{reader.GetString(0)}, {reader.GetString(1)}");

            }
        }
    }
}
