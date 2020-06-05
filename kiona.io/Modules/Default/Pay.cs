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
    public class Pay : ModuleBase<SocketCommandContext>
    {
        [Command("Pay")]
        public async Task PayUser(SocketGuildUser user, double amount)
        {
            var handler = new UserAccounts();

            if(handler.GetUserAccount(Context.User.Id) is null || handler.GetUserAccount(user.Id) is null || handler.GetUserAccount(Context.User.Id).Balance < amount || amount < 1)
            {
                await Context.Channel.SendMessageAsync("", false, Utilities.SendEmbedMessage("◆ Could not pay this user.", Utilities.Colors.Red));
                return;
            }

            handler.TakeUserAccountBalance(Context.User.Id, amount);
            handler.AddUserAccountBalance(user.Id, amount);

            await Context.Channel.SendMessageAsync("", false, Utilities.SendEmbedMessage($"◆ You paid **{user.Username}** ${amount}.", Utilities.Colors.Green));
        }
    }
}