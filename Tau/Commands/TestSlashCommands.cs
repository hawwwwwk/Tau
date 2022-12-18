// UNUSED


//using DSharpPlus.Entities;
//using DSharpPlus;
//using DSharpPlus.SlashCommands;

//namespace Tau.Commands
//{
//    public class TestSlashCommands : ApplicationCommandModule
//    {
//        // sum
//        [SlashCommand("sum", "takes the sum of two numbers")]
//        public async Task SumCommand(InteractionContext ctx,
//            [Option("num1", "num1")] long num1,
//            [Option("num2", "num2")] long num2
//            )
//        {
//            int output = (int)num1 + (int)num2;
//            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, 
//                new DiscordInteractionResponseBuilder().WithContent("" + output));
//        } 

//        // 8ball
//        [SlashCommand("8ball", "Ask Mark to provide his wisdom.")]
//        public async Task EightBallCommand(InteractionContext ctx, [Option("Question", "Question")] String inputNotUsed)
//        {
//            Random rnd = new Random();
//            string[] eightBallResponseOptions =
//            {
//                "It is certain.",
//                "It is decidedly so.",
//                "Without a doubt.",
//                "Yes definitely.",
//                "You may rely on it.",

//                "As I see it, yes.",
//                "Most likely.",
//                "Outlook good.",
//                "Yes.",
//                "Signs point to yes.",

//                "Don't count on it.",
//                "My reply is no.",
//                "My sources say no.",
//                "Outlook not so good.",
//                "Very doubtful."
//            };
//            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, 
//                new DiscordInteractionResponseBuilder().WithContent(eightBallResponseOptions[rnd.Next(0, 14)]));
//        }
//    }
//}
