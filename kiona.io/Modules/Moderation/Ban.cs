using Discord.Commands;
using Discord.WebSocket;
using kiona.io.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kiona.io.Modules.Moderation
{
    public class Ban : ModuleBase<SocketCommandContext>
    {
        [Command("Ban")]
        public async Task BanUser(SocketGuildUser user = null, [Remainder] string reason = "No reason provided.")
        {
            if(!Utilities.HasRoles(Context.User as SocketGuildUser, new List<string> { "Admin", "Founder" }))
            {
                return;
            }

            if(user is null)
            {
                await Context.Channel.SendMessageAsync("", false, Utilities.SendEmbedMessage($"**Could not execute the command.**\n\nUsage: {Configuration.Configuration.Prefix}Ban <User/Id> <Reason> ", Utilities.Colors.Red));
                return;
            }

            await user.Guild.AddBanAsync(user, 0, reason);

            await Context.Channel.SendMessageAsync("", false, Utilities.SendEmbedMessage($"Successfully banned *{user.Username}*.", Utilities.Colors.Green));

        }
    }
}
