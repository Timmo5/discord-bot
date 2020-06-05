using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kiona.io.Modules.Default
{
    public class TailsOrHeads : ModuleBase<SocketCommandContext>
    {
        [Command("TailsOrHeads"), Alias("toh")]
        public async Task TailsOrHeadsModule()
        {
            const string PATH = @"C:\Users\timot\Documents\C#\kiona.io\kiona.io\Resources\";
            bool chance = new Random().Next(2) == 0;

            if(chance)
            {
                await Context.Channel.SendFileAsync($"{PATH}tails.png");
            }
            else
            {
                await Context.Channel.SendFileAsync($"{PATH}heads.png");
            }
        }
    }
}