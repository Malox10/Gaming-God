using System;
using System.Collections.Generic;
using System.IO;

namespace GamingGod {
    public class Database {
        public Dictionary<long, Student> students = new Dictionary<long, Student>();
        public const string configpath = "StundenplanConfig.txt";  //to make things less cluttered
        private const int configlength = 10; //so i can change the length easier but it's a bit of a hassle because you need to manually change the config for now
        public string[] text;
        public string discordtoken;

        public Database() {
            if (!File.Exists(configpath)) {
                File.Create(configpath);
                Console.WriteLine("Pls Enter DiscordToken in First Line of " + configpath + " and restart.");
                Console.ReadLine();
                Environment.Exit(0);
            }
            Load();
            Test();
        }

        public void Test() {
            var x = new Student(23232323232323223);
            Console.WriteLine(TimeTable.Print(x.timetable)); 

            Console.WriteLine(TimeTable.Print(students[191526817599848458].timetable));
        }

        public void Load() {
            text = File.ReadAllLines(configpath);
            discordtoken = text[0]; //you need your own discord token
            //
            //read Settings section. idk if i need it tho.
            //
            List<string[]> students = new List<string[]>();
            for (int i = 0; i < text.Length; i++) {//used for and not foreach because i need access to the counting variable
                if (text[i].EndsWith("]")) {       //if there's a user name put the next "configlength"(10) lines into a string array
                    int counter = i;
                    string[] locallines = new string[configlength];
                    for (; i < counter + configlength; i++) {
                        locallines[i - counter] = text[i];
                        if (i == counter + (configlength - 1)) students.Add(locallines);
                    }
                }
            }
            foreach (var item in students) {
                long ID = long.Parse(item[0].Substring(1, item[0].Length - 2));//gets name out of first line
                var student = new Student(ID);                            //creates new student adds them to the database and procedes to fill in all the information
                this.students.Add(ID, student);
                TimeTable.ParseTimeTable(item, student.timetable);
            }
        }
    }
}