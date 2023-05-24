using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // connect to server PostgreSQL
            string connectionString = "Server=localhost; Port=5432; Database=test; UserId=postgres; Password=0227D_k4545a; commandTimeout=120;";
            // string connectionString = "Server=localhost;Username=postgres;Password=0227D_k4545a;Database=test;";
            var conn = PostgressUtils.Connect(connectionString);
            Console.ReadLine();
        }
    }
}
