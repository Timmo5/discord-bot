using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.IO;
using kiona.io.Configuration;
using kiona.io.Filter;

namespace kiona.io
{
    public class CommandHandler
    {
        static void Main(string[] args)
        {
            try
            {
                new CommandHandler().StartAsync().GetAwaiter().GetResult();
            }
            catch(Exception e)
            {
                TerminalManager.Log($"Failed to launch: {e.GetBaseException()}", TerminalManager.Severity.Error);
                Console.ReadLine();
                return;
            }
        }

        private DiscordSocketClient client;
        private CommandService commands;
        private IServiceProvider services;

        private async Task StartAsync()
        {
            var config = new Configuration.Configuration();

            client = new DiscordSocketClient();
            commands = new CommandService();
            services = new ServiceCollection()
                .AddSingleton(client).AddSingleton(commands).BuildServiceProvider();

            var data = JsonConvert.DeserializeObject<Configuration.API>(File.ReadAllText(@"C:\Users\timot\Documents\C#\kiona.io\kiona.io\Configuration\Data.json"));
            if(data.Moderation.filter)
            {
                var filter = new Filter.Filter(client);
            }

            TerminalManager.Log(data.Moderation.filter ? "Chat filter is ON" : "Chat filter is OFF", TerminalManager.Severity.Info);
            client.Log += Log;

            await RegisterCommandsAsync();
            await client.LoginAsync(TokenType.Bot, Configuration.Configuration.Token);
            await client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage arg)
        {
            TerminalManager.Log(arg.Message, TerminalManager.Severity.Info);
            return Task.CompletedTask;
        }

        private async Task RegisterCommandsAsync()
        {
            client.MessageReceived += MessageReceivedAsync;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly(), services);
        }


        private async Task MessageReceivedAsync(SocketMessage arg)
        {
            if(!(arg is SocketUserMessage message) || message.Author.IsBot)
            {
                return;
            }

            int argPos = 0;

            if(message.HasCharPrefix(Configuration.Configuration.Prefix, ref argPos))
            {
                var context = new SocketCommandContext(client, message);
                var result = await commands.ExecuteAsync(context, argPos, services);
                if(!result.IsSuccess)
                {
                    TerminalManager.Log($"Command Failure: {result.ErrorReason}", TerminalManager.Severity.Warning);
                }
            }
        }
    }
}
