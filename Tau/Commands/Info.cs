using DSharpPlus.Entities;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tau.Commands
{
    // info
    [SlashCommandGroup("info", "info aboot stufs")]
    public class Info : ApplicationCommandModule
    {
        // server
        [SlashCommand("server", "displays server info")]
        public async Task ServerCommand(InteractionContext ctx)
        {
            var eb = new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Aquamarine)
                .WithDescription("test")
                .Build();

            await ctx.CreateResponseAsync(eb, false);
        }
        
        
    }
}
