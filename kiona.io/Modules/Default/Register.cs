using Discord.Commands;
using kiona.io.UserAccountHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kiona.io.Modules.Default
{
    public class Register : ModuleBase<SocketCommandContext>
    {
        [Command("Register")]
        public async Task RegisterUser()
        {
            var handler = new UserAccounts();

            if(handler.GetUserAccount(Context.User.Id) is null)
            {
                handler.AddUserAccount(Context.User.Id, 1000);
                await Context.Channel.SendMessageAsync($"Successfully registered with the ID `{Context.User.Id}`.");
            }
            else
            {
                await Context.Channel.SendMessageAsync("You are already registered.");
            }
        }
    }
}