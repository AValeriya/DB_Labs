using Npgsql;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace DB_client
{

    static class PostgressUtils
    {
        public static NpgsqlConnection Connect(string connectionString)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            try
            {
                //Console.WriteLine("Openning Connection ...");

                conn.Open();

                //Console.WriteLine("Connection successful!");
            }
            catch (Exception e)
            {
                throw new Exception("Error connecting to the database", e);
            }
            return conn;
        }

        public static async Task ExecuteSelectAsReader(NpgsqlConnection conn, string sql, Action<DbDataReader> callback)
        {
            using (var command = conn.CreateCommand())
            {
                command.CommandText = sql;
                using (var reader = await command.ExecuteReaderAsync())
                {
                    callback(reader);
                }
            }
        }
    }
}
