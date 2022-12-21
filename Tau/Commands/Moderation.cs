// will finish later

//using DSharpPlus.Entities;
//using DSharpPlus.SlashCommands;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics.SymbolStore;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Tau.Commands;
//[SlashCommandGroup("mod", "Set of mod-only commands.")]
//public class Moderation : ApplicationCommandModule
//{
//    [SlashCommand("user", "Provides useful info about a user.")]
//    public async Task UserCommand(InteractionContext ctx,
//        [Option("userId", "ID of user you're checking.")]string userId,
//        [Option("silent", "Whether or not this message should be Ephemeral.")]bool silent = false)
//    {
//        var eb = new DiscordEmbedBuilder()
//            .WithColor(DiscordColor.Blue)
//            .Build();
//        await ctx.CreateResponseAsync(eb, silent);
//    }
//}
