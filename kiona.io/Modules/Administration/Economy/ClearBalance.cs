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
    public class ClearBalance : ModuleBase<SocketCommandContext>
    {
        [Command("ClearBalance"), Alias("clearbal")]
        public async Task ClearUserBalance(SocketGuildUser user)
        {
            if(!Utilities.UserHasPermission(Context.User as SocketGuildUser, GuildPermission.Administrator))
            {
                return;
            }

            var handler = new UserAccounts();

            handler.ClearUserAccountBalance(user.Id);
            await Context.Channel.SendMessageAsync($"Cleared **{user.Username}**'s balance!");
        }
    }
}