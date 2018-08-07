using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading;
using System.Threading.Tasks;

namespace GamingGod {
    public class GamingGod {
        Database database;
        const string urlToday = "http://www.vp-gas-merzig.de/schueler/heuteS.pdf";
        const string urlTomorrow = "http://www.vp-gas-merzig.de/schueler/morgenS.pdf";
        public GamingGod() {
            database = new Database();
            new Thread(DiscordBot.Connect(database.discordtoken).GetAwaiter().GetResult).Start();
        }

        public void StartTracking () {
            while (true) {
                CheckForAffectedStudents(new RepTable(urlToday));
                CheckForAffectedStudents(new RepTable(urlTomorrow));
                Task.Delay(6000).Wait();
            }
        }

        public void CheckForAffectedStudents(RepTable repTable) {
            foreach (var entry in repTable.entries) {
                var matches = new List<Student>();
                foreach (var student in database.students.Values) {
                    if (student.timetable[entry.lesson - 1, (int)repTable.dateTarget.DayOfWeek - 1].teacher == entry.teacher) {
                        if (!student.sentNotifications.Contains(entry)) {
                            matches.Add(student);
                            student.sentNotifications.Add(entry);
                        }
                    }
                }
                if (matches.Count > 0) {
                    var subject = matches[0].timetable[entry.lesson - 1, (int)repTable.dateTarget.DayOfWeek - 1].subject;
                    var message = "";
                    foreach (var student in matches) {
                        message += $"<@{student.discordID}> ";
                    }
                    message += Environment.NewLine;
                    if (DateTime.Now.Day == repTable.dateTarget.Day) {
                        message += "**Heute** ";
                    }
                    else if (DateTime.Now.Day == repTable.dateTarget.Day - 1) {
                        message += "**Morgen** ";
                    }
                    else {
                        message += "**Irgendwann** ";
                    }

                    if (entry.subject == "---") {
                        message += $"fällt in der **{entry.lesson}.** Stunde **{subject}** bei **{entry.teacher}** aus!";
                    }
                    else {
                        message += $"wird **{subject}** bei **{entry.teacher}** von **{entry.substitude}** in **{entry.room}** vertreten.";
                    }
                    message += Environment.NewLine;
                    if (entry.info != "") {
                        message += $"*{entry.info}*";
                    }
                    else {
                        message += "*[kein Grund angegeben]*";
                    }
                    DiscordHook.channel.SendMessageAsync(message);
                }
            }
        }
    }
}
