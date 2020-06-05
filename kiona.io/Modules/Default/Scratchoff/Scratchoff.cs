using Discord;
using Discord.Commands;
using kiona.io.UserAccountHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kiona.io.Modules.Default.Scratchoff
{
    public class Scratchoff : ModuleBase<SocketCommandContext> 
    {
        public Result GetResult()
        {
            var random = new Random();

            int probability = random.Next(0, 100);
                        
            if(probability > 40)
            {
                int lProbability = random.Next(1, 3);

                switch(lProbability)
                {
                    case 1:
                        return new Result()
                        {
                            IsWin = false,
                            Url = "https://cdn.discordapp.com/attachments/649750818349776907/653588152870633502/scratchoff-losvar1.png",
                            Amount = 0.0
                        };
                    case 2:
                        return new Result()
                        {
                            IsWin = false,
                            Url = "https://cdn.discordapp.com/attachments/649750818349776907/653588153822740480/scratchoff-losvar2.png",
                            Amount = 0.0
                        };
                    case 3:
                        return new Result()
                        {
                            IsWin = false,
                            Url = "https://cdn.discordapp.com/attachments/649750818349776907/653588155936800790/scratchoff-losvar3.png",
                            Amount = 0.0
                        };
                }
            }
            else if (probability > 20 && probability < 40)
            {
                return new Result()
                {
                    IsWin = true,
                    Url = "https://cdn.discordapp.com/attachments/649750818349776907/653588156184133642/scratchoff-winvar1.png",
                    Amount = 250.0
                };
            }
            else if(probability > 5 && probability < 20)
            {
                return new Result()
                {
                    IsWin = true,
                    Url = "https://cdn.discordapp.com/attachments/649750818349776907/653588157236772864/scratchoff-winvar2.png",
                    Amount = 500.0
                };
            }
            else if(probability < 5)
            {
                return new Result()
                {
                    IsWin = true,
                    Url = "https://cdn.discordapp.com/attachments/649750818349776907/653588159036391425/scratchoff-winvar3.png",
                    Amount = 1000.0
                };
            }

            return null;
        }

        [Command("Scratchoff"), Alias("lotto")]
        public async Task ScratchoffCasino()
        {
            var handler = new UserAccounts();

            if(handler.GetUserAccount(Context.User.Id).Balance < 100)
            {
                await Context.Channel.SendMessageAsync("", false, Utilities.SendEmbedMessage("◆ You can't afford purchasing a scratch off ticket.", Utilities.Colors.Red));
                return;
            }

            handler.TakeUserAccountBalance(Context.User.Id, 100);

            var result = GetResult();

            var embed = new EmbedBuilder()
            {
                Color = new Color(255, 255, 255),
                Author = new EmbedAuthorBuilder()
                {
                    IconUrl = Context.User.GetAvatarUrl(),
                    Name = Context.User.ToString()
                },
                Title = result.IsWin ? $"You bought a scratch off ticket and won ${result.Amount}!" : "You bought a scratch off ticket and lost. Better luck next time!",
                ImageUrl = result.Url,
            };

            if(result.Amount > 0)
            {
                handler.AddUserAccountBalance(Context.User.Id, result.Amount);
            }

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
    }
}