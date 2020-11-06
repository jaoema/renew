using Npgsql;
using System;

namespace CallSqlFunctions
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "host=localhost;db=imdb;uid=postgres;pwd =Baad666";
            UseAdo(connectionString);

        }

        private static void UseAdo(string connectionString)
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            //var cmd = new NpgsqlCommand("select * from find_coplayers('%ab%')", connection);
            //var cmd = new NpgsqlCommand("select * from name_search('hans1','Mads')", connection);

            //hans1 needs to be replaced by Username later on. So that it works for everyone.
            var cmd = new NpgsqlCommand("select * from name_search('hans1','%ab%')", connection);


            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"{reader.GetString(0)}, {reader.GetString(1)}");

            }
        }
    }
}
