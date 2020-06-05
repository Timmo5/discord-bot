using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kiona.io.Modules.Default
{
    public class Help : ModuleBase<SocketCommandContext>
    {
        [Command("Help"), Alias("helpMePlease")]
        public async Task HelpCommand()
        {
            await Context.Channel.SendMessageAsync("", false,
                Utilities.SendEmbedMessage("**Help:**" +
                "\n\n◆ !register - Register your account to be eligible to use the economy." +
                "\n◆ !balance [user] - Shows yours or another user's balance." +
                "\n◆ !pay [user] [amount] - Pays another user from your balance." +
                "\n◆ !help - Displays this help message." +
                "\n◆ !hot - Flip a coin Heads or Tails.", Utilities.Colors.Blue));
        }
    }
}