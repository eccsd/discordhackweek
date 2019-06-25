using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace RTXBot
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;
        public static DateTime exectime = DateTime.Now;

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();
            string botToken = "ENTER_TOKEN_HERE";
            string release = "1"; string majorVersion = "0"; string minorVersion = "0";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Maui, the only bot you need in your life! v{release}.{majorVersion}.{minorVersion}");
            Console.WriteLine("");
            Console.ResetColor();
            _client.Log += Log;
            _client.MessageReceived += ThankYou;
            await _client.LoginAsync(TokenType.Bot, botToken);
            await _client.StartAsync();
            await _client.SetGameAsync("#DiscordHackWeek");
            await Task.Delay(-1);
        }

        private async Task ThankYou(SocketMessage msg)
        {
            DateTime time = DateTime.Now;
            var message = msg as SocketUserMessage;
            string[] thanks = { "thanks", "thx", "fanks", "thank", "fank", "what can i say", " ty", "ty " };
            if (thanks.Any(message.Content.ToLower().Contains))
            {
                await message.Channel.SendMessageAsync("https://tenor.com/UvCg.gif");
            }
        }

        public Task Log(Discord.LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }
    }
}

