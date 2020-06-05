using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace kiona.io.Modules.Administration
{
    public class SendMessage : ModuleBase<SocketCommandContext>
    {
        public enum Colors
        {
            Red,
            Green,
            Blue
        }

        [Command("SendMessage"), Alias("S")]
        public async Task SendAdministrationMessage(ITextChannel channel = null, Colors color = Colors.Green, string pingType = null, [Remainder] string message = null)
        {
            if(!Utilities.UserHasPermission(Context.User as SocketGuildUser, GuildPermission.Administrator))
            {
                return;
            }

            if(channel is null || pingType is null || message is null)
            {
                await Context.Channel.SendMessageAsync("", false, Utilities.SendEmbedMessage($"**Could not execute the command.**\n\nUsage: {Configuration.Configuration.Prefix}SendMessage <Channel> <Color> <PingType> <Message>\n\nColors: Red/Green/Blue\nPing Types: Everyone/Here/None", Utilities.Colors.Red));
                return;
            }

            var e = new EmbedBuilder()
            {
                Description = message
            };

            switch(color)
            {
                case Colors.Red:
                    e.Color = new Color(200, 51, 51);
                    break;
                case Colors.Green:
                    e.Color = new Color(51, 200, 51);
                    break;
                case Colors.Blue:
                    e.Color = new Color(0, 191, 255);
                    break;
            }

            switch(pingType.ToLower())
            {
                case "everyone":
                    pingType = "everyone";
                    break;
                case "here":
                    pingType = "here";
                    break;
                case "none":
                    pingType = null;
                    break;
                default:
                    pingType = null;
                    break;
            }

            if(pingType != null)
            {
                var ping = await channel.SendMessageAsync("@" + pingType);
                await ping.DeleteAsync();
            }

            await channel.SendMessageAsync("", false, e.Build());
        }
    }
}