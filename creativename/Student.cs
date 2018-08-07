using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingGod {
    public class Student {
        public long discordID = 0;
        public Lesson[,] timetable = new Lesson[8, 5];
        public List<Entry> sentNotifications = new List<Entry>();

        public Student(long ID) {
            discordID = ID;
            TimeTable.FillTimeTable(timetable);
        }
    }
}