using System;
using System.Collections.Generic;
using System.IO;

namespace GamingGod {
    public static class Config {
        private static bool file_exists = false;     //to avoid exceptions when there's no file
        public const string configpath = "StundenplanConfig.txt";  //to make things less cluttered
        private const int configlength = 10; //so i can change the length easier but it's a bit of a hassle because you need to manually change the config for now
        public static string[] text;

        public static void CreateFile() {
            File.Create(configpath);
            file_exists = true;
            Console.WriteLine("Config Created!");
        }

        public static void ConfigMain() {
            file_exists = File.Exists(configpath);
            if (file_exists) {
                Console.WriteLine("Config exists");
            }
            else CreateFile();
            ReadConfig();
            Console.WriteLine(Database.studentdatabase["Maximilian"].timetable.Print());
        }

        public static void ReadConfig() {
            //
            //read Settings section. idk if i need it tho.
            //
            if (file_exists) {
                text = File.ReadAllLines(configpath);
                List<string[]> students = new List<string[]>();
                for (int i = 0; i < text.Length; i++) {//used for and not foreach because i need access to the counting variable
                    if (text[i].EndsWith("]")) {        //if there's a user name put the next "configlength"(10) lines into a string array
                        int counter = i;
                        string[] locallines = new string[configlength];
                        for (; i < counter + configlength; i++) {
                            locallines[i - counter] = text[i];
                            if (i == counter + (configlength - 1)) students.Add(locallines);
                        }
                    }
                }
                foreach (var item in students) {
                    string name = item[0].Substring(1, item[0].Length - 2);//gets name out of first line
                    var x = new Students(name);                            //creates new student adds them to the database and procedes to fill in all the information
                    Database.studentdatabase.Add(name, x);
                    x.FillInfo(item);
                }
            }
            else {//if it the config doesn't exist just create a new and tell the user
                Console.WriteLine("File doesn't exist");
                CreateFile();
            }
        }
    }
}