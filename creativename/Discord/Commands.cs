﻿using Discord;
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
            //DONT FORGET THIS!!!
            //i'll probably forget this tho :(
        }

        [Command("thischannel")]//prints out free lessons in this channel
        public async Task Thischannel() {
            DiscordHook.channel = Context.Channel;
            await DiscordHook.channel.SendMessageAsync("Aye Aye Captain!");
            new Thread(Program.gg.StartTracking).Start();
        }

        [Command("changeprefix")]
        public async Task Changeprefix(string prefix) {
            CommandHandler.prefix = prefix;
            await Context.Channel.SendMessageAsync("New prefix is `" + prefix + "`");
        }

        [Command("sendpm")]
        public async Task Sendpm() {
            //await Context.User.SendMessageAsync("test");
           var x = await Context.User.GetOrCreateDMChannelAsync();
            await x.SendMessageAsync("test");
        }

        [Command("test")]
        public async Task Test() {
            await Context.Channel.SendMessageAsync("Test <@"+ Context.Channel  +">");
        }
    }
}

#region outdated code

//changed dictionary to List and want to handle everything manually at first



// [Command("adduser")]
// public async Task AddUser(string name) {
//     if (Database.studentdatabase.ContainsKey(name)) await Context.Channel.SendMessageAsync("User does already exist.");
//     else {
//         var x = new Students(name) {
//             discordID = (long)Context.User.Id
//         };
//         Database.studentdatabase.Add(name, x);
//         await Context.Channel.SendMessageAsync("User : `" + name + "` created.");
//     }
// }
// 
// [Command("converttodiscorduser")]
// public async Task ConvertToDiscordUser (string name){
//     if (!Database.studentdatabase.ContainsKey(name)) await Context.Channel.SendMessageAsync("User does not exist.");
//     else {
//         Database.studentdatabase[name].discordID = (long)Context.User.Id;
//         await Context.Channel.SendMessageAsync("conversion complete.");
//     }
// }
// 
// [Command("deleteuser")]
// public async Task DeleteUser(string name) {
//     if (!Database.studentdatabase.ContainsKey(name)) await Context.Channel.SendMessageAsync("User does not exist.");
//     else {
//         if (Database.studentdatabase[name].CommandCreated) {
//             var x = new Random();
//             int rng = x.Next(1000, 9999);
//             var deletion = new ConfirmationProcess {
//                 confirmation = rng,
//                 userid = Context.User.Id,
//                 channel = Context.Channel,
//                 name = name
//             };
//             confirmationprocesses.Add(deletion);
//             await Context.Channel.SendMessageAsync("Are you sure to delete this user ?");
//             await Context.Channel.SendMessageAsync("Type : `" + CommandHandler.prefix + "confirm" + rng + "` or `exit` to exit deletion.");
//         }
//         else await Context.Channel.SendMessageAsync("Cannot delete users that are not created via `" + CommandHandler.prefix + "adduser` with`" + CommandHandler.prefix + "`deleteuser");
//     }
// }
//
// [Command("confirm")]
// public async Task Confirm(string number) {
//     if (confirmationprocesses.Count > 0) {
//         foreach (var x in confirmationprocesses) {
//             if (x.userid == Context.User.Id) {
//                 Int32.TryParse(number, out int y);
//                 if (x.confirmation == y) {
//                     confirmationprocesses.Remove(x);
//                     Database.studentdatabase.Remove(x.name);
//                     await Context.Channel.SendMessageAsync("User : `" + x.name + "` successfully deleted");
//                 }
//                 else if (number == "exit") {
//                     confirmationprocesses.Remove(x);
//                     await Context.Channel.SendMessageAsync("Deletion abborted.");
//                 }
//             }
//        }
//     }
// }
//
// [Command("timetable")]
// public async Task Timetable(string task,string user) {
//     if (!Database.studentdatabase.ContainsKey(user)) await Context.Channel.SendMessageAsync("User does not exist.");
//     if (task == "show") await Context.Channel.SendMessageAsync("`" + Database.studentdatabase[user].timetable.Print() + "`");
// }
#endregion