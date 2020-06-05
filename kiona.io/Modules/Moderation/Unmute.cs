using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kiona.io.Modules.Moderation
{
    public class Unmute : ModuleBase<SocketCommandContext>
    {
        [Command("Unmute")]
        public async Task UnmuteUser(SocketGuildUser user = null) {
            if(!Utilities.HasRoles(Context.User as SocketGuildUser, new List<string> { "Admin", "Founder" }))
            {
                return;
            }

            if(user is null)
            {
                await Context.Channel.SendMessageAsync("", false, Utilities.SendEmbedMessage($"**Could not execute the command.**\n\nUsage: {Configuration.Configuration.Prefix}Unmute <User/Id>", Utilities.Colors.Red));
                return;
            }

            var mutedRole = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Muted");
            var memberRole = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Member");

            await user.RemoveRoleAsync(mutedRole);
            await user.AddRoleAsync(memberRole);

            await Context.Channel.SendMessageAsync("", false, Utilities.SendEmbedMessage($"Successfully unmuted *{user.Username}*.", Utilities.Colors.Green));

        }
    }
}
