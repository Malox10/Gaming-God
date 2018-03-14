using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GamingGod {
    class Program {
        static void Main(string[] args) {
            var x = new TimeTable();
            Console.WriteLine(x.Print());
            new Thread(new ThreadStart(DiscordBot.Connect().GetAwaiter().GetResult));
            Console.ReadLine();
        }
    }
}