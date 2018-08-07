using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GamingGod {
    public class RepTable {
        public DateTime dateTarget, datePublished;
        public List<Entry> entries = new List<Entry>();
        private static Regex roomRegex = new Regex(@"^[A-Z0-9]{4}");

        public RepTable(string url) {
            var doc = PDDocument.load(new java.net.URL(url));
            var lines = new PDFTextStripper().getText(doc).Split('\n');
            var isEntry = false;
            var isDate = false;
            foreach (var line in lines) {
                Console.WriteLine(line);
                if (line.StartsWith("Druck")) {
                    var d = line.Substring(line.IndexOf("Klasse") + 7).Split('.');
                    dateTarget = new DateTime(
                        datePublished.Year,
                        int.Parse(d[1]),
                        int.Parse(d[0])
                    );

                }
                if (isDate) {
                    var halves = line.Split(' ');
                    if (halves[1] == "") halves[1] = halves[2];
                    var d = halves[0].Split('.');
                    var t = halves[1].Split(':');
                    datePublished = new DateTime(
                        int.Parse(d[2]),
                        int.Parse(d[1]),
                        int.Parse(d[0]),
                        int.Parse(t[0]),
                        int.Parse(t[1]),
                        0
                    );
                    isDate = false;
                }
                if (!isEntry || line.StartsWith("Seite")) {
                    isEntry = line.StartsWith("Klasse(n)");
                    isDate = line.StartsWith("Stundenplan");
                    continue;
                }
                var sections = line.Split(' ').ToList();
                if (sections[2] == "-") {
                    sections.RemoveAt(2);
                    sections[1] += "-" + sections[2];
                    sections.RemoveAt(2);
                }
                if (sections[0].Length != 2) {
                    continue;
                }
                while (!roomRegex.IsMatch(sections[3]) && sections[3] != "---") {
                    sections[2] += sections[3];
                    sections.RemoveAt(3);
                }
                if (char.IsLower(sections[4][1])) {
                    sections.RemoveAt(4);
                    sections.RemoveAt(4);
                    sections.RemoveAt(4);
                }
                var info = "";
                while (sections.Count > 6) {
                    if (sections[6].Contains("\r")) {
                        info += sections[6].Replace("\r", "");
                    }
                    else {
                        info += sections[6] + " ";
                    }
                    sections.RemoveAt(6);
                }
                var entry = new Entry() {
                    grade = sections[0],
                    substitude = sections[2],
                    room = sections[3],
                    subject = sections[4],
                    teacher = sections[5],
                    info = info,
                };
                if (int.TryParse(sections[1], out int x)) {
                    entry.lesson = x;
                    entries.Add(entry);
                }
                else if (sections[1].Length == 3) {
                    entry.lesson = int.Parse(""+sections[1][0]);
                    entries.Add(entry);
                    var entry2 = entry;
                    entry2.lesson = int.Parse("" + sections[1][2]);
                    entries.Add(entry2);
                }
                else {
                    //unexpected
                }
            }
            doc.close();
        }
    }

    public struct Entry {
        public string teacher;
        public string subject;
        public string room;
        public string grade;
        public int lesson;
        public string substitude;
        public string info;
    }
}
