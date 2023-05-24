using DB_client;
using Npgsql;
using System;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Transactions;
using System.Xml.Xsl;
using static DB_client_net.Log;
using System.Collections.Concurrent;
using System.Xml.Linq;
using System.Runtime.Serialization;

namespace DB_client_net
{
    static class Program
    {
        static NpgsqlConnection con;

        static List<string> cart = new List<string>();

        static void Main(string[] args)
        {
            bool admin = false;

            string connectionString = $"Server=localhost; Port=5432; Database=FirstDB; UserId=postgres; Password=0227D_k4545a; commandTimeout=120;";

            if (!File.Exists("log.txt"))
            {
                File.Create("log.txt");
            }

            try
            {
                con = PostgressUtils.Connect(connectionString);

                while (true)
                {
                    WriteLine("Enter command");
                    if (admin)
                    {
                        Write("admin> ");
                    }
                    string command = ReadLine();
                    if (command.Length == 0) continue;

                    if (command == "help")
                    {
                        WriteLine("Commands are: admin, log off, games, companies, developers, users, orders, cart, editor");
                    }
                    else if (command == "exit")
                    {
                        break;
                    }
                    else if (command == "admin")
                    {
                        if (!admin)
                        {
                            WriteLine("Enter passsword:");
                            string pass = ReadLine();
                            if (pass == "42")
                            {
                                WriteLine("You are now admin!");
                                admin = true;
                            }
                            else
                            {
                                WriteLine("Wrong password!");
                            }
                        }
                        else
                        {
                            WriteLine("You are already admin!");
                        }
                    }
                    else if (command == "log off")
                    {
                        if (admin)
                        {
                            admin = false;
                        }
                    }
                    else if (command == "cart")
                    {
                        Order();
                    }
                    else if (command == "games")
                    {
                        string sql = "SELECT * FROM game ORDER BY id";
                        using var cmd = new NpgsqlCommand(sql, con);

                        using NpgsqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            WriteLine(string.Format($"{rdr.GetString(1)} ({rdr.GetInt32(2)}$), genre: {rdr.GetString(3)}, country: {rdr.GetString(5)}"));
                        }
                    }
                    else if (command == "companies")
                    {
                        string sql = "SELECT * FROM company ORDER BY id";
                        using var cmd = new NpgsqlCommand(sql, con);

                        using NpgsqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            WriteLine(string.Format($"{rdr.GetString(1)}"));
                        }
                    }
                    else if (command == "developers")
                    {
                        string sql = "SELECT * FROM developer ORDER BY id";
                        using var cmd = new NpgsqlCommand(sql, con);

                        using NpgsqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            WriteLine(string.Format($"{rdr.GetString(1)}"));
                        }
                    }
                    else if (command == "editor")
                    {
                        if (admin)
                        {
                            Editor();
                        }
                        else
                        {
                            WriteLine("You not an admin!");
                        }
                    }
                    else if (command == "users")
                    {
                        if (admin)
                        {
                            string sql = "SELECT * FROM users ORDER BY id";
                            using var cmd = new NpgsqlCommand(sql, con);

                            using NpgsqlDataReader rdr = cmd.ExecuteReader();

                            while (rdr.Read())
                            {
                                WriteLine(string.Format($"User({rdr.GetInt32(0)}): {rdr.GetString(1)}"));
                            }
                        }
                        else
                        {
                            WriteLine("You not an admin!");
                        }
                    }
                    else if (command == "orders")
                    {
                        if (admin)
                        {
                            string sql = "SELECT * FROM orders ORDER BY id";
                            using var cmd = new NpgsqlCommand(sql, con);

                            using NpgsqlDataReader rdr = cmd.ExecuteReader();

                            while (rdr.Read())
                            {
                                WriteLine(string.Format($"Order({rdr.GetInt32(0)}) price: {rdr.GetInt32(1)}, status: {rdr.GetString(2)}"));
                            }
                        }
                        else
                        {
                            WriteLine("You not an admin!");
                        }
                    }
                    else
                    {
                        var cmd = new NpgsqlCommand("", con);
                        cmd.CommandText = command;
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception)
                        {
                            WriteLine("Incorrect command!");
                        }
                    }

                }
            }
            catch (Exception)
            {
                WriteLine("Something went wrong");
            }

            Console.ReadLine();
        }

        static void Order()
        {
            string sql = "SELECT * FROM game ORDER BY id";
            using var cmd = new NpgsqlCommand(sql, con);

            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            List<string> games = new List<string>();

            while (rdr.Read())
            {
                games.Add(rdr.GetString(1));
            }

            while (true)
            {
                WriteLine($"Your cart has {cart.Count} items\n1. view games\n2. view cart\n3. add game\n4. purchase");
                string c = ReadLine();
                if (c == "exit")
                {
                    break;
                }
                if (int.TryParse(c, out int cat))
                {
                    switch (cat)
                    {
                        case 1:
                            WriteLine("Games:");
                            for (int i = 1; i <= games.Count; i++)
                            {
                                WriteLine($"{i}: {games[i - 1]}");
                            }
                            break;
                        case 2:
                            WriteLine("Cart:");
                            for (int i = 1; i <= cart.Count; i++)
                            {
                                WriteLine($"{i}: {cart[i - 1]}");
                            }
                            break;
                        case 3:
                            {
                                WriteLine("Select game number:");
                                string i = ReadLine();
                                if (int.TryParse(i, out int index))
                                {
                                    if (index >= 1 && index <= games.Count)
                                    {
                                        cart.Add(games[index - 1]);
                                    }
                                    else
                                    {
                                        WriteLine("Wrong index");
                                    }
                                }
                                else
                                {
                                    WriteLine("Wrong index");
                                }
                            }
                            break;
                        case 4:
                            string list = "";
                            cart.ForEach(g => list += $" {g}");
                            WriteLine($"You have purchased these games: {list}");
                            cart.Clear();
                            break;
                    }
                }
            }
        }

        static void Editor()
        {
            while (true)
            {
                WriteLine("Select category:\n1. games\n2. developers");
                string c = ReadLine();
                if (c == "exit")
                {
                    break;
                }
                if (int.TryParse(c, out int cat))
                {
                    switch (cat)
                    {
                        case 1:
                            Games();
                            break;
                        case 2:
                            Companies();
                            break;
                    }
                }
            }
        }

        static void Games()
        {
            while (true)
            {
                WriteLine("Actions:\n1. view games\n2. add a game\n3. edit games");
                var c = ReadLine();
                if (c == "exit")
                {
                    break;
                }
                if (int.TryParse(c, out int cat))
                {
                    switch (cat)
                    {
                        case 1:
                            ViewGames();
                            break;
                        case 2:
                            AddGame();
                            break;
                        case 3:
                            EditGames();
                            break;
                    }
                }
            }
        }

        static void ViewGames()
        {
            string sql = "SELECT * FROM game ORDER BY id";
            using var cmd = new NpgsqlCommand(sql, con);

            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                WriteLine(string.Format($"id: {rdr.GetInt32(0)} {rdr.GetString(1)} ({rdr.GetInt32(2)}$), genre: {rdr.GetString(3)}, country: {rdr.GetString(5)}"));
            }
        }

        static void AddGame()
        {
            WriteLine("Enter game name:");
            string name = ReadLine();
            WriteLine("Enter price:");
            string price = ReadLine();
            WriteLine("Enter genre:");
            string genre = ReadLine();
            WriteLine("Enter lang:");
            string lang = ReadLine();
            WriteLine("Enter country:");
            string country = ReadLine();

            using var cmd = new NpgsqlCommand("", con);

            cmd.CommandText = "SELECT COUNT(*) FROM game ORDER BY id";
            var id = cmd.ExecuteScalar();

            cmd.CommandText = $"INSERT INTO game VALUES ({id},'{name}',{price},'{genre}','{lang}','{country}',1,1,1,1,1,1)";
            cmd.ExecuteNonQuery();
        }

        static void EditGames()
        {
            WriteLine("Enter game numner:");
            var i = ReadLine();
            if (int.TryParse(i, out int index))
            {
                int id = index;

                WriteLine("Enter game name:");
                string name = ReadLine();
                WriteLine("Enter price:");
                string price = ReadLine();
                WriteLine("Enter genre:");
                string genre = ReadLine();
                WriteLine("Enter lang:");
                string lang = ReadLine();
                WriteLine("Enter country:");
                string country = ReadLine();

                using var cmd = new NpgsqlCommand("", con);

                cmd.CommandText = "SELECT COUNT(*) FROM game ORDER BY id";
                //var id = cmd.ExecuteScalar();

                cmd.CommandText = $"UPDATE game SET gamename='{name}', prices={price}, genre='{genre}', languages='{lang}', country='{country}' where id = {id}";
                cmd.ExecuteNonQuery();
            }
        }

        static void Companies()
        {
            while (true)
            {
                WriteLine("Actions:\n1. view companies\n2. add a company\n3. edit companies");
                var c = ReadLine();
                if (c == "exit")
                {
                    break;
                }
                if (int.TryParse(c, out int cat))
                {
                    switch (cat)
                    {
                        case 1:
                            ViewCompanies();
                            break;
                        case 2:
                            AddCompany();
                            break;
                        case 3:
                            EditCompany();
                            break;
                    }
                }
            }
        }

        static void ViewCompanies()
        {
            string sql = "SELECT * FROM company ORDER BY id";
            using var cmd = new NpgsqlCommand(sql, con);

            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                WriteLine(string.Format($"id: {rdr.GetInt32(0)} {rdr.GetString(1)}"));
            }
        }

        static void AddCompany()
        {
            WriteLine("Enter company name:");
            string name = ReadLine();

            using var cmd = new NpgsqlCommand("", con);

            cmd.CommandText = "SELECT COUNT(*) FROM game ORDER BY id";
            var id = cmd.ExecuteScalar();

            cmd.CommandText = $"INSERT INTO company VALUES ({id},'{name}')";
            cmd.ExecuteNonQuery();
        }

        static void EditCompany()
        {
            WriteLine("Enter company numner:");
            var i = ReadLine();
            if (int.TryParse(i, out int index))
            {
                int id = index;

                using var cmd = new NpgsqlCommand("", con);

                WriteLine("Enter company name:");
                string name = ReadLine();

                cmd.CommandText = $"UPDATE company SET companyname='{name}' WHERE id={id}";
                cmd.ExecuteNonQuery();
            }
        }
    }
}
