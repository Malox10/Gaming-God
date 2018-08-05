using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingGod {
    public class Students {
        public string grade;
        public string name = "";
        public long discordID = 0;
        public string discordtag = "";
        public TimeTable timetable = new TimeTable();
        public bool CommandCreated {
            get => discordID == 1;
        }

        public Students(string word) {
            name = word;
        }

        public void FillInfo(string[] stringarray) {
            grade = stringarray[6];
            if(stringarray[7] != "") discordID = Int64.Parse(stringarray[7]); //don't want to cause exceptions
            discordtag = stringarray[8];
            //stringarray[9];
            timetable.FillTimeTable(stringarray);
        }
    }
}