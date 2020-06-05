using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace kiona.io.Filter
{
    public class Filter : ModuleBase<SocketCommandContext>
    {
        private DiscordSocketClient _client;

        public Filter(DiscordSocketClient client)
        {
            _client = client;
            _client.MessageReceived += MessageReceivedAsync;
        }

        private async Task MessageReceivedAsync(SocketMessage arg)
        {
            foreach (var word in Words.FilteredWords)
            {
                if(arg.Content.ToLower().Contains(word))
                {
                    if(Utilities.HasRoles(arg.Author as SocketGuildUser, new List<string> { "Admin", "Founder" })) {
                        break;
                    }
                    await arg.DeleteAsync();
                }
            }

            foreach(var attachment in arg.Attachments)
            {
                var url = new Uri($"http://api.rest7.com/v1/detect_nudity.php?url={attachment.Url}");
                using(var client = new WebClient())
                {
                    var data = JsonConvert.DeserializeObject<API>(client.DownloadString(url));
                    if(data.nudity)
                    {
                        await arg.DeleteAsync();
                        break;
                    }
                }
            }
        }
    }
}