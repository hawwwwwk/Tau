using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

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
#if DEBUG
                .WithFooter("Tau v0.0.31, Beta Branch") // todo: automate the changing of this lol
#else
                .WithFooter("Tau v0.0.31")
#endif
                .Build();
            await ctx.CreateResponseAsync(eb, false);
        }

        [SlashCommand("rps", "Plays rock paper scissors with Tau!")]
        public static async Task RPSCommand(InteractionContext ctx)
        {
            var emojis = new Dictionary<string, DiscordEmoji>
                {
                    { "rock", DiscordEmoji.FromName(ctx.Client, ":rock:") },
                    { "paper", DiscordEmoji.FromName(ctx.Client, ":page_facing_up:") },
                    { "scissors", DiscordEmoji.FromName(ctx.Client, ":scissors:") }
                };

            var eb = new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Aquamarine)
                .WithTitle("Rock, paper, scissors!")
                .Build();
            var buttons = new List<DiscordButtonComponent>();
            foreach (var emoji in emojis)
            {
                buttons.Add(new DiscordButtonComponent(DSharpPlus.ButtonStyle.Primary, emoji.Key, $"{emoji.Value}"));
            }

            await ctx.CreateResponseAsync("", embed: eb);
        }
    }
}
