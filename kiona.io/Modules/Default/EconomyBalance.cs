using Discord.Commands;
using Discord.WebSocket;
using kiona.io.UserAccountHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kiona.io.Modules.Default
{
    public class EconomyBalance : ModuleBase<SocketCommandContext>
    {
        [Command("Balance"), Alias("bal")]
        public async Task Balance(SocketGuildUser user = null)
        {
            var handler = new UserAccounts();

            if(user is null)
            {
                await Context.Channel.SendMessageAsync($"Your balance is: ${Convert.ToDecimal(handler.GetUserAccount(Context.User.Id).Balance).ToString("#,##0.00")}");
            }
            else
            {
                await Context.Channel.SendMessageAsync($"Balance of **{user.Username}:** ${Convert.ToDecimal(handler.GetUserAccount(user.Id).Balance).ToString("#,##0.00")}");
            }
        }
    }
}