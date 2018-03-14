using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingGod {
    public class TimeTable {
        public Lesson[,] timetable = new Lesson[8, 5];

        public TimeTable() {
            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < 8; j++) {
                    var x = ((i + 1) * 10) + (j + 1);
                    timetable[j, i].subject = x.ToString();
                    timetable[j, i].teacher = "  ";
                }
            }
        }

        public string Print() {
            string y = "-";
            string x = "";
            for (int j = 0; j < 8; j++) {
                x += "\n| ";
                for (int i = 0; i < 5; i++) {
                    x += timetable[j, i].Name + " | ";
                    if(j == 0) y += "---------";
                }
            }
            var z = y;
            y += x;
            y += "\n" + z;
            return y;
        }
    }

    public struct Lesson {
        public string teacher;
        public string subject;
        public string Name {
            get {
                var x = subject;
                if (subject.Length == 3) x += " ";
                else x += "  ";
                x += teacher;
                return x;
            }
        }
    }
}