using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kiona.io.UserAccountHandler;

namespace kiona.io.Modules.Administration.Economy
{
    public class AddBalance : ModuleBase<SocketCommandContext>
    {
        [Command("AddBalance"), Alias("addbal")]
        public async Task AddUserBalance(SocketGuildUser user, int amount = 0)
        {
            if(user is null || amount > int.MaxValue || amount < 1 || !Utilities.UserHasPermission(Context.User as SocketGuildUser, GuildPermission.Administrator))
            {
                return;
            }

            var handler = new UserAccounts();

            handler.AddUserAccountBalance(user.Id, amount);
            
            await Context.Channel.SendMessageAsync($"Added ${Convert.ToDecimal(amount).ToString("#,##0.00")} to **{user.Username}**.");
        }
    }
}