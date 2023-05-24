using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_client_net
{
    internal static class Log
    {
        internal static string ReadLine()
        {
            string s = Console.ReadLine();

            using (var fs = new FileStream("log.txt", FileMode.Append, FileAccess.Write))
            {
                fs.Write(Encoding.Unicode.GetBytes(s + '\n'));
            }

            return s;
        }

        internal static void Write(string s)
        {
            Console.Write(s);

            using (var fs = new FileStream("log.txt", FileMode.Append, FileAccess.Write))
            {
                fs.Write(Encoding.Unicode.GetBytes(s));
            }
        }

        internal static void WriteLine(string s)
        {
            Console.WriteLine(s);

            using (var fs = new FileStream("log.txt", FileMode.Append, FileAccess.Write))
            {
                fs.Write(Encoding.Unicode.GetBytes(s + '\n'));
            }
        }

    }
}
