using Discord.Commands;
using Discord.WebSocket;
using kiona.io.UserAccountHandler;
using System;
using System.Threading.Tasks;

namespace kiona.io.Modules.Default
{
    public class Balance : ModuleBase<SocketCommandContext>
    {
        [Command("Balance"), Alias("bal")]
        public async Task UserBalance(SocketGuildUser user = null)
        {
            var handler = new UserAccounts();

            if(user is null)
            {
                if(handler.GetUserAccount(Context.User.Id) is null)
                {
                    return;
                }

                await Context.Channel.SendMessageAsync("", false, Utilities.SendEmbedMessage($"◆ Your balance is: ${handler.GetUserAccount(Context.User.Id).Balance}", Utilities.Colors.Green));
            }
            else
            {
                if(handler.GetUserAccount(user.Id) is null)
                {
                    return;
                }

                await Context.Channel.SendMessageAsync("", false, Utilities.SendEmbedMessage($"◆ **{user.Username}'s** balance: ${handler.GetUserAccount(user.Id).Balance}", Utilities.Colors.Green));
                Console.WriteLine(handler.GetUserAccount(user.Id).Balance);
            }
        }
    }
}