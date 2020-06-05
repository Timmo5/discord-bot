using Discord;
using Discord.Commands;
using Discord.WebSocket;
using kiona.io.UserAccountHandler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kiona.io.Modules.Administration.Economy
{
    public class Database : ModuleBase<SocketCommandContext>
    {
        [Command("Database")]
        public async Task GetDatabase()
        {
            if(!Utilities.UserHasPermission(Context.User as SocketGuildUser, GuildPermission.Administrator))
            {
                return;
            }

            await Context.Channel.SendMessageAsync("```" + new UserAccounts().GetData() + "```");
        }
    }
}