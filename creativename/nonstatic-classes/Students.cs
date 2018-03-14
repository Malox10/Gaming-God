using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingGod {
    public class Students {
        public string name = "";
        public string email = "";
        public string discordtag = "";
        public ulong discordID = 0;
        public List<string> subjects;
        public TimeTable timetable = new TimeTable();
        public bool CommandCreated {
            get => discordID == 0;
        }
    }
}
