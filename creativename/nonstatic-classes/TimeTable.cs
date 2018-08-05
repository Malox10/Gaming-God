﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingGod {
    public class TimeTable {
        public Lesson[,] timetable = new Lesson[8, 5];

        public TimeTable() {//fills in numbers first digit ist the column / day, second digit ist the row / nth lesson
            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < 8; j++) {
                    var x = ((i + 1) * 10) + (j + 1);
                    timetable[j, i].subject = x.ToString();
                    timetable[j, i].teacher = "   ";
                }
            }
        }

        public string Print() {
            //prints out timetable
            string y = "-------";
            string x = "";
            for (int j = 0; j < 8; j++) { //outer loop prints rows
                x += "\n" + (j + 1) + ".)" + "  |";
                for (int i = 0; i < 5; i++) { //inner loops prints columns
                    x += " " + timetable[j, i].Name + " |";//prints 5 times subject + teacher then "|"
                    if(j == 0) y += "----------";
                }
            }
            var z = y;
            y += x;
            y += "\n" + z;
            return y;
        }

        public void FillTimeTable(string[] stringarray) {
            for (int i = 1; i <= 5; i++) { //this iterates through all 5 days
                var lessonday = stringarray[i].Split(':');
                int counter = 0;
                for (int j = 0; j < lessonday.Length; j++) {//this iterated through all lessons
                    string[] lessons = lessonday[j].Split('.');
                    var x = new Lesson {
                        subject = lessons[0],
                        teacher = lessons[1]
                    };
                    for (int k = 0; k < Int32.Parse(lessons[2]); k++) {//this repeats if we have a double lesson
                        timetable[counter, i - 1] = x;
                        counter++;
                    }
                }
            }
        }
    }

    public struct Lesson {
        public string teacher;
        public string subject;
        public string Name {
            get {
                var x = subject;
                x += " ";//if both are length 3 i don't want them to stick together
                if (subject.Length == 2) x += " "; //some subjects are 3 characters long instead of 3
                if (teacher.Length == 2) x += " "; //so u don't missalign the timetable
                x += teacher;
                return x;
            }
        }
    }
}