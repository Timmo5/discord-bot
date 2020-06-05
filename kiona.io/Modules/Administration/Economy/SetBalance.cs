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
    public class SetBalance : ModuleBase<SocketCommandContext>
    {
        [Command("SetBalance"), Alias("setBal")]
        public async Task SetUserBalance(SocketGuildUser user, double amount)
        {
            if(!Utilities.UserHasPermission(Context.User as SocketGuildUser, GuildPermission.Administrator))
            {
                return;
            }

            new UserAccounts().SetUserAccountBalance(user.Id, amount);
            await Context.Channel.SendMessageAsync("", false, Utilities.SendEmbedMessage($"◆ Successfully set **{user.Username}'s** balance to ${amount}.", Utilities.Colors.Green));
        }
    }
}