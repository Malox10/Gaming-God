using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GamingGod {
    public static class Database {
        public static Dictionary<string, Students> studentdatabase = new Dictionary<string, Students>();
        public static Dictionary<string, Lesson> lessondatabase = new Dictionary<string, Lesson>();
    }
}
