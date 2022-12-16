using DSharpPlus.Entities;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// todo, seperate slash command groups (not [slashcommandgroup] they suck) into seperate files, basically abstract a lil.

namespace Tau.Commands
{
    public class General : ApplicationCommandModule
    {
        [SlashCommand("about", "Displays information about Tau.")]
        public async Task AboutCommand(InteractionContext ctx)
        {
            var eb = new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Aquamarine)
                .WithTitle("Tau")
                .WithDescription("Tau is a general purpose bot written in C# using [DSharpPlus](https://dsharpplus.github.io/)!")

                .AddField("Developers", "[hawk](https://github.com/hawwwwwk)")
                .AddField("Very special thanks:", "A huge thank you to everyone in the [DSharpPlus Discord Server](https://discord.gg/dsharpplus) for helping me learn this library, and thanks to [Nimrod](https://discord.gg/vSmKPNd4) for letting me host on their servers.")
                .AddField("Questions? Comments?", "If you're having any issues with Tau or have a suggestion, join our [Support Server](https://discord.gg/EatSTE2u)!")

                .WithFooter("Tau v0.0.2")

                .Build();

            await ctx.CreateResponseAsync(eb, false);
        }
    }    
}
