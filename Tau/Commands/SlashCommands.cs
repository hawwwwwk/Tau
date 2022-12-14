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
    public class SlashCommands : ApplicationCommandModule
    {
        [SlashCommand("about", "Displays information about Tau.")]
        public async Task AboutCommand(InteractionContext ctx)
        {
            var eb = new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Aquamarine)
                .WithTitle("Tau")
                .WithDescription("Tau is a general purpose bot written in C# using [DSharpPlus](https://dsharpplus.github.io/)!")

                .AddField("Developers", "[hawk](https://github.com/hawwwwwk)")
                .AddField("Very special thanks:", "A huge thank you to everyone in the [DSharpPlus Discord Server](https://discord.gg/dsharpplus) for helping me learn this library and grow without spoonfeeding me. Especially huge thanks to Naamloos and their bot [Mod Core](https://github.com/Naamloos/ModCore), which has open source code that gave me a visual reference of how I should structure my code.")
                .AddField("Questions? Comments?", "If you're having any issues with Tau or have a suggestion, feel free to join our [Support Server](https://discord.gg/EatSTE2u)!")

                .WithFooter("Tau v00.1")

                .Build();

            await ctx.CreateResponseAsync(eb, false);
        }
    }    
}
