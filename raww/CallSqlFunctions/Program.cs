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
            //Name_Search(connectionString);
            //Find_Popular_Actors(connectionString);
            //Add_Rating_History(connectionString);
            Create_User(connectionString);
            //Rate(connectionString);
            //Login(connectionString);
            //Name_Rating(connectionString);
           
        }

        //This method searches for a name, the ref for this method is NS

        public static void Name_Search(string connectionString)
        {
            var ctx = new ImdbContext(connectionString);
            var result = ctx.Name_Search.FromSqlInterpolated($"select * from name_search('hans1','Mads')");

            foreach (var Name_Search in result)
            {
                Console.WriteLine($"{ Name_Search.nconst}, { Name_Search.primaryname}");
            }
        }
        // Finds popular actors the ref for this method is FPA
        private static void Find_Popular_Actors(string connectionString)
        {
            var ctx = new ImdbContext(connectionString);
            var result = ctx.Find_Popular_Actors.FromSqlInterpolated($"select * from popular_actors(10) ");

            foreach (var Find_Popular_Actors in result)
            {
                Console.WriteLine($"{ Find_Popular_Actors.primaryname}, {Find_Popular_Actors.rating}");
            }
        }
       
        private static void Add_Rating_History(string connectionString)
        {

            var ctx = new ImdbContext(connectionString);
            var connection = (NpgsqlConnection)ctx.Database.GetDbConnection();
            connection.Open();
            var cmd = new NpgsqlCommand($"select add_rating_history('hans1', 'tt10850402', 9)", connection);
            cmd.ExecuteNonQuery();
        }

        private static void Create_User(string connectionString)
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

        //Bookmark
        //Find_Coplayers

        private static void Login(string connectionString)
        {
            var ctx = new ImdbContext(connectionString);
            var connection = (NpgsqlConnection)ctx.Database.GetDbConnection();
            connection.Open();
            var cmd = new NpgsqlCommand($"select login('rasmus2', 'baad')", connection);
            cmd.ExecuteNonQuery();
                        
        }

        //Find out what this returns and how to get it. 
        private static void Name_Rating(string connectionString)
        {
            var ctx = new ImdbContext(connectionString);
            var result = ctx.Name_Rating.FromSqlInterpolated($"select name_rating('Mads Mikkelsen')");

            foreach (var Name_Rating in result)
            {
                Console.WriteLine($"{ Name_Rating.primaryname}");
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
