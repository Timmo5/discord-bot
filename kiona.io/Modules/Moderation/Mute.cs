using Discord.Commands;
using Discord.WebSocket;
using kiona.io.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kiona.io.Modules.Moderation
{
    public class Mute : ModuleBase<SocketCommandContext>
    {
        [Command("Mute")]
        public async Task MuteUser(SocketGuildUser user = null, [Remainder] string reason = "No reason provided.")
        {
            if(!Utilities.HasRoles(Context.User as SocketGuildUser, new List<string> { "Admin", "Founder" }))
            {
                return;
            }
            
            if(user is null)
            {
                await Context.Channel.SendMessageAsync("", false, Utilities.SendEmbedMessage($"**Could not execute the command.**\n\nUsage: {Configuration.Configuration.Prefix}Mute < User/Id> <Reason> ", Utilities.Colors.Red));
                return;
            }

            var mutedRole = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Muted");
            var memberRole = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Member");

            await user.AddRoleAsync(mutedRole);
            await user.RemoveRoleAsync(memberRole);

            await Context.Channel.SendMessageAsync("", false, Utilities.SendEmbedMessage($"Successfully muted *{user.Username}*.", Utilities.Colors.Green));

        }
    }
}
