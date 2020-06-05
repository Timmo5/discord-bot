using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kiona.io.Modules.Administration
{
    public class AdministratorHelp : ModuleBase<SocketCommandContext>
    {
        [Command("AdministratorHelp"), Alias("aHelp")]
        public async Task AdministratorHelpCommand()
        {
            await Context.Channel.SendMessageAsync("", false, Utilities.SendEmbedMessage("**Admin Help:**" +
                "\n\n◆ !setbal [user] [amount] - Sets the balance of a user." +
                "\n◆ !addbal [user] [amount] - Adds money to a user's balance." +
                "\n◆ !takebal [user] [amount] - Take money from a user's balance." +
                "\n◆ !resetbal [user] - Resets the balance of a user to the default balance." +
                "\n◆ !database - Shows the database.", Utilities.Colors.Blue));
        }
    }
}