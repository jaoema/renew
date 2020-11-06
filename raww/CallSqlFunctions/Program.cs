using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;

namespace CallSqlFunctions
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "host=localhost;db=imdb;uid=postgres;pwd =Baad666";



            //(UseAdo(connectionString);
            //NS(connectionString);
            //FPA(connectionString);
            ARH(connectionString);

        }

        //This method searches for a name, the ref for this method is NS
        private static void NS(string connectionString)
        {
            var ctx = new ImdbContext(connectionString);
            var result = ctx.Name_Search.FromSqlInterpolated($"select * from name_search('hans1','Mads')");

            foreach (var Name_Search in result)
            {
                Console.WriteLine($"{ Name_Search.nconst}, { Name_Search.primaryname}");
            }
        }

        // Finds popular actors the ref for this method is FPA
        private static void FPA(string connectionString)
        {
            var ctx = new ImdbContext(connectionString);
            var result = ctx.Find_Popular_Actors.FromSqlInterpolated($"select * from popular_actors(10) ");

            foreach (var Find_Popular_Actors in result)
            {
                Console.WriteLine($"{ Find_Popular_Actors.primaryname}, {Find_Popular_Actors.rating}");
            }
        }
        //DUR IKKE
        private static void ARH(string connectionString)
        {
            using var ctx = new ImdbContext(connectionString);
            Ratinghistory r = new Ratinghistory
            {
                username = "jaja",
                rating = 4,
                tconst = "tconst"
            };

            //ctx.Ratinghistories.Add(new Ratinghistory{username = "username", rating = 5, tconst = "tconst"  });
            ctx.Ratinghistories.Attach(r);
            ctx.SaveChanges();

           

            //var result = ctx.Add_Rating_History.FromSqlInterpolated($"insert into ratinghistory('username', 'movie', 'rating')");

            //foreach (var ARH in result)
            // {
            //    Console.WriteLine($"{ Add_Rating_History.username}, {Add_Rating_History.movie}, {Add_Rating_History.rating}");
            //}

        }


        private static void UseAdo(string connectionString)
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            //var cmd = new NpgsqlCommand("select * from find_coplayers('%ab%')", connection);
            //var cmd = new NpgsqlCommand("select * from name_search('hans1','Mads')", connection);

            //hans1 needs to be replaced by Username later on. So that it works for everyone.
            var cmd = new NpgsqlCommand("select * from name_search(hans1,%ab%)", connection);

            /*
            var username = "hans1";
            var primname = "mads";

            var cmd = new NpgsqlCommand("select * from name_search(username,primname)", connection);
            */

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"{reader.GetString(0)}, {reader.GetString(1)}");

            }
        }
    }
}
