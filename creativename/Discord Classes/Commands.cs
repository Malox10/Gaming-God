using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GamingGod {
    public class Commands : ModuleBase<SocketCommandContext> {
        public static List<ConfirmationProcess> confirmationprocesses = new List<ConfirmationProcess>();

        [Command("help")]
        public async Task Help() {
            var author = new EmbedAuthorBuilder {
                Name = "help",
                IconUrl = "https://avatars0.githubusercontent.com/u/32681550?s=460&v=4",
                Url = "https://github.com/Malox10/Gaming-God"//shameless self advertising
            };
            var embed = new EmbedBuilder();
            embed.WithAuthor(author);
            embed.Description = "test";
            await Context.Channel.SendMessageAsync("", false, embed);
            //need to work on
        }

        [Command("thischannel")]
        public async Task Thischannel() {
            DiscordHook.channel = Context.Channel;
            await Context.Channel.SendMessageAsync("Aye Aye Captain!");
        }

        [Command("changeprefix")]
        public async Task Changeprefix(string prefix) {
            CommandHandler.prefix = prefix;
            await Context.Channel.SendMessageAsync("New prefix is `" + prefix + "`");
        }

        [Command("adduser")]
        public async Task AddUser(string name) {
            if (Database.studentdatabase.ContainsKey(name)) {
                await Context.Channel.SendMessageAsync("User does already exist.");
            }
            else {
                var x = new Students {
                    name = name
                };
                Database.studentdatabase.Add(name, x);
                await Context.Channel.SendMessageAsync("User : `" + name + "` created.");
            }
        }

        [Command("deleteuser")]
        public async Task DeleteUser(string name) {
            if (!Database.studentdatabase.ContainsKey(name)) {
                await Context.Channel.SendMessageAsync("User does not exist.");
            }
            else {
                if (Database.studentdatabase[name].CommandCreated) {
                    var x = new Random();
                    int rng = x.Next(1000, 9999);
                    var deletion = new ConfirmationProcess {
                        confirmation = rng,
                        userid = Context.User.Id,
                        channel = Context.Channel,
                        name = name
                    };
                    confirmationprocesses.Add(deletion);
                    await Context.Channel.SendMessageAsync("Are you sure to delete this user ?");
                    await Context.Channel.SendMessageAsync("Type : `" + CommandHandler.prefix + "confirm" + rng + "` or `exit` to exit deletion.");
                }
                else {
                    await Context.Channel.SendMessageAsync("Cannot delete users that are not created via `" + CommandHandler.prefix + "adduser` with`" + CommandHandler.prefix + "`deleteuser");
                }
            }
        }

        [Command("confirm")]
        public async Task Confirm(string number) {
            if (confirmationprocesses.Count > 0) {
                foreach (var x in confirmationprocesses) {
                    if (x.userid == Context.User.Id) {
                        Int32.TryParse(number, out int y);
                        if (x.confirmation == y) {
                            confirmationprocesses.Remove(x);
                            Database.studentdatabase.Remove(x.name);
                            await Context.Channel.SendMessageAsync("User : `" + x.name + "` successfully deleted");
                        }
                        else if (number == "exit") {
                            confirmationprocesses.Remove(x);
                            await Context.Channel.SendMessageAsync("Deletion abborted.");
                        }
                    }
                }
            }
        }

        [Command("timetable")]
        public async Task Timetable(string user,string task) {
            if (Database.studentdatabase.ContainsKey(user)) {
                await Context.Channel.SendMessageAsync("User does not exist.");
            }
            if (task == "show") {

            }
        }
    }
}