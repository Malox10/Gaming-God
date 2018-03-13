using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;


namespace GamingGod {
    class DiscordHook {
        public static ISocketMessageChannel channel;

        public static void Confirmation(ConfirmationProcess c, SocketCommandContext context) {

        }
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
        }
    }
}
    #endregion