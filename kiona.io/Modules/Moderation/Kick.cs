using Discord.Commands;
using Discord.WebSocket;
using kiona.io;
using kiona.io.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace why.Modules.Moderation
{
    public class Kick : ModuleBase<SocketCommandContext>
    {
        [Command("Kick")]
        public async Task KickUser(SocketGuildUser user = null, [Remainder] string reason = "No reason provided.")
        {
            if(!Utilities.HasRoles(Context.User as SocketGuildUser, new List<string> { "Admin", "Founder" }))
            {
                return;
            }

            if(user is null)
            {
                await Context.Channel.SendMessageAsync("", false, Utilities.SendEmbedMessage($"**Could not execute the command.**\n\nUsage: {Configuration.Prefix}Kick <User/Id> <Reason> ", Utilities.Colors.Red));
                return;
            }

            await user.KickAsync(reason);

            await Context.Channel.SendMessageAsync("", false, Utilities.SendEmbedMessage($"Successfully kicked *{user.Username}*.", Utilities.Colors.Green));

        }
    }
}