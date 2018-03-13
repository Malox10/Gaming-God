using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GamingGod {
    public class Commands : ModuleBase<SocketCommandContext> {
        public static List<ConfirmationProcess> confirmationprocesses = new List<ConfirmationProcess>();

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
            if (Database.database.ContainsKey(name)) {
                await Context.Channel.SendMessageAsync("User does already exist.");
            }
            else {
                var x = new Students {
                    name = name
                };
                Database.database.Add(name, x);
                await Context.Channel.SendMessageAsync("User : `" + name + "` created.");
            }
        }

        [Command("deleteuser")]
        public async Task DeleteUser(string name) {
            if (!Database.database.ContainsKey(name)) {
                await Context.Channel.SendMessageAsync("User does not exist.");
            }
            else {
                if (Database.database[name].CommandCreated) {
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
                            Database.database.Remove(x.name);
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
    }
}