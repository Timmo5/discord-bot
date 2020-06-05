using Discord;
using Discord.Commands;
using Discord.WebSocket;
using kiona.io.UserAccountHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kiona.io.Modules.Administration.Economy
{
    public class ResetBalance : ModuleBase<SocketCommandContext>
    {
        [Command("ResetBalance"), Alias("resetBal")]
        public async Task ClearUserBalance(SocketGuildUser user)
        {
            if(!Utilities.UserHasPermission(Context.User as SocketGuildUser, GuildPermission.Administrator))
            {
                return;
            }

            _ = new UserAccounts().ResetUserAccountBalance(user.Id);

            await Context.Channel.SendMessageAsync("", false, Utilities.SendEmbedMessage($"◆ Successfully reset **{user.Username}'s** balance.", Utilities.Colors.Green));
        }
    }
}