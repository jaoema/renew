using System;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Collections.Generic;
using System.Text;

namespace SqlFunctions
{
    public class Functions
    {
        static void Main(string[] args)
        {
            var connectionString = "host=localhost;db=imdb;uid=postgres;pwd =Baad666";
            //var connectionString = "host=localhost;db=amdb;uid=postgres;pwd =Franet0365";

            //(UseAdo(connectionString);
            NameSearch(connectionString);
            //FindPopularActors(connectionString);
            //AddRatingHistory(connectionString);
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
            var result = ctx.NameSearch.FromSqlInterpolated($"select * from name_search({usernam},{searchterm})");

            foreach (var NameSearch in result)
            {
                Console.WriteLine($"{ NameSearch.nconst}, { NameSearch.primaryname}");
            }
        }
        // Finds popular actors the ref for this method is FPA
        public static void FindPopularActors(string connectionString)
        {
            var ctx = new ImdbContext(connectionString);
            var result = ctx.FindPopularActors.FromSqlInterpolated($"select * from popular_actors(10) ");

            foreach (var FindPopularActors in result)
            {
                Console.WriteLine($"{ FindPopularActors.primaryname}, {FindPopularActors.rating}");
            }
        }

        public static void AddRatingHistory(string connectionString)
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

        public static void CreateUser(string connectionString)
        {
            var ctx = new ImdbContext(connectionString);
            var connection = (NpgsqlConnection)ctx.Database.GetDbConnection();
            connection.Open();
            var cmd = new NpgsqlCommand($"select create_user('ddheas', 'eerrrghg')", connection);
            cmd.ExecuteNonQuery();
        }

        public static void Rate(string connectionString)
        {
            var ctx = new ImdbContext(connectionString);
            var connection = (NpgsqlConnection)ctx.Database.GetDbConnection();
            connection.Open();
            var cmd = new NpgsqlCommand($"select rate('hans1', 'tt11097072', 2)", connection);
            cmd.ExecuteNonQuery();
        }

        public static void Bookmark(string connectionString)
        {
            var ctx = new ImdbContext(connectionString);
            var connection = (NpgsqlConnection)ctx.Database.GetDbConnection();
            connection.Open();
            var cmd = new NpgsqlCommand($"select bookmark('alex5', 'Mads Mikkelsen', 'Act')", connection);
            cmd.ExecuteNonQuery();

        }

        //Find_Coplayers

        public static void Login(string connectionString)
        {
            var ctx = new ImdbContext(connectionString);
            var connection = (NpgsqlConnection)ctx.Database.GetDbConnection();
            connection.Open();
            var cmd = new NpgsqlCommand($"select login('rasmus2', 'baad')", connection);
            cmd.ExecuteNonQuery();

        }


        public static void NameRating(string connectionString)
        {
            var ctx = new ImdbContext(connectionString);
            var result = ctx.NameRating.FromSqlInterpolated($"select name_rating('Mads Mikkelsen')");

            foreach (var NameRating in result)
            {
                Console.WriteLine($"{ NameRating.primaryname}");
            }
        }

        public static void StringSearch(string connectionString)
        {
            var ctx = new ImdbContext(connectionString);
            var result = ctx.StringSearch.FromSqlInterpolated($"select * from string_search('hans1','vampire')");

            foreach (var StringSearch in result)
            {
                Console.WriteLine($"{StringSearch.tconst}, { StringSearch.primarytitle}");
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
