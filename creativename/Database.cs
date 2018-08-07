using System;
using System.Collections.Generic;
using System.IO;

namespace GamingGod {
    public class Database {
        public Dictionary<long, Student> students = new Dictionary<long, Student>();
        public const string configpath = "StundenplanConfig.txt";  //to make things less cluttered
        private const int configlength = 6; //so i can change the length easier but it's a bit of a hassle because you need to manually change the config for now
        public string[] text;
        public string discordtoken;

        /// <summary>
        /// The Config  works in the following way:
        /// 
        /// The discord token is stored in its first line
        /// Each Entry starts with a number in brackets followed by the users timetable i.e
        /// 
        /// [88492837499832748458]
        /// DE.EFC.3.A001:BEF.DF.3.B001
        /// DE.EFC.3.A001:BEF.DF.3.B001
        /// DE.EFC.3.A001:BEF.DF.3.B001
        /// DE.EFC.3.A001:BEF.DF.3.B001
        /// DE.EFC.3.A001:BEF.DF.3.B001
        /// 
        /// one line represents one day Every lesson block is seperated by ':' 
        /// and the information in each lesson block is seperated by '.'
        /// The first row would mean the following :
        ///  We have 2 lesson blocks :
        ///  DE.EFC.3.A001
        ///  BEF.DF.3.B001
        ///  the first set of characters represent your subject
        ///  in this case DE and BEF. the abbreviations are just random in this example
        ///  
        ///  EFC and DF represent the abbreviation of the teacher 
        ///  
        ///  The third info is always a single digit and represents the number of lessons 
        ///  that are after each other. So both lesson blocks consist of 3 lessons after another
        ///  
        ///  And the last entry is the abbreviation for the room in this case A001 and B001.
        ///  
        /// What this entry would tell you that every day of the week you have your first three 
        /// lessons in room A001 with the teacher EFC in subject DE and the next three lessons
        /// in room B001 with teacher DF in the subject BEF
        /// </summary>

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