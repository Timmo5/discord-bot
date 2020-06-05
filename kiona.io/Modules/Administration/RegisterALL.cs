using Discord;
using Discord.Commands;
using Discord.WebSocket;
using kiona.io.UserAccountHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kiona.io.Modules.Administration
{
    public class RegisterALL : ModuleBase<SocketCommandContext>
    {
        [Command("RegisterALL_register")]
        public async Task RegisterALLCommand()
        {
            if(!Utilities.UserHasPermission(Context.User as SocketGuildUser, GuildPermission.Administrator))
            {
                return;
            }

            int userCount = 0;

            foreach(var user in Context.Guild.Users)
            {
                userCount++;
                var handler = new UserAccounts();

                handler.AddUserAccount(user.Id, 200);
            }

            await Context.Channel.SendMessageAsync(userCount.ToString());
        }
    }
}