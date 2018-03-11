using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace GamingGod {
    class DiscordHook {
        public static ISocketMessageChannel channel;
    }
    #region DiscordBot
    public static class DiscordBot {
        static private DiscordSocketClient _client;
        static private CommandHandler _handler;

        public static async Task Connect() {
            _client = new DiscordSocketClient(new DiscordSocketConfig {
                WebSocketProvider = Discord.Net.Providers.WS4Net.WS4NetProvider.Instance
            });
            _handler = new CommandHandler(_client);
            await _client.LoginAsync(TokenType.Bot, File.ReadAllText("DiscordToken.txt"));
            await _client.StartAsync();
            await Task.Delay(-1);
        }
    }
    #endregion
    #region CommandHandler
    public class CommandHandler {
        private DiscordSocketClient _client;
        CommandService _service;
        public static string prefix = "!";


        public CommandHandler(DiscordSocketClient client) {

            _client = client;
            _service = new CommandService();
            _service.AddModulesAsync(Assembly.GetEntryAssembly());
            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage s) {
            var msg = s as SocketUserMessage;
            var context = new SocketCommandContext(_client, msg);

            if (msg == null) return;
            if (msg.Author.Id == _client.CurrentUser.Id || msg.Author.IsBot) return;
            int argPos = 0;
            if (msg.HasStringPrefix(prefix, ref argPos)) {
                var result = await _service.ExecuteAsync(context, argPos);
                if (!result.IsSuccess) {
                    await context.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
            if (Commands.deletionprocesses.Count > 0) {
                var result = await _service.ExecuteAsync(context, argPos);
            }
        }
    }
    #endregion
    #region Commands
    public class Commands : ModuleBase<SocketCommandContext> {
        public static List<DeletionProcess> deletionprocesses = new List<DeletionProcess>();

        [Command("thischannel")]
        public async Task Thischannel(string word) {
            if (word == "report") {
                DiscordHook.channel = Context.Channel;
                await Context.Channel.SendMessageAsync("Aye Aye Captain!");
            }
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
                var x = new Random();
                int rng = x.Next(1000, 9999);
                var deletion = new DeletionProcess {
                    confirmation = rng,
                    userid = Context.User.Id,
                    channel = Context.Channel,
                    name = name
                };
                deletionprocesses.Add(deletion);
                await Context.Channel.SendMessageAsync("Are you sure to delete this user ?");
                await Context.Channel.SendMessageAsync("Type : `" + rng + "` in the next 10 seconds to confirm.");
                //Thread.Sleep(10000); need to test
                await Task.Delay(10000);
                if (!deletion.success) {
                    deletionprocesses.Remove(deletion);
                    await Context.Channel.SendMessageAsync("Deletion cancelled.");
                }
            }
        }

        [Command()]
        public async Task Confirmation(string text) {
            foreach (var y in deletionprocesses) {
                if (y.userid == Context.User.Id) {
                    if (Int32.TryParse(text, out int x)) {
                        if (y.confirmation == x) {
                            deletionprocesses.Remove(y);
                            Database.database.Remove(y.name);
                            y.success = true;
                            await Context.Channel.SendMessageAsync("User : `" + y.name + "` successfully deleted");
                        }
                    }
                    else if (text == "exit") {
                        deletionprocesses.Remove(y);
                        y.success = true;
                        await Context.Channel.SendMessageAsync("Deletion cancelled.");
                    }
                }
            }
        }
    }
}
#endregion