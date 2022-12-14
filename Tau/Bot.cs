using DSharpPlus;
using System;
using System.Threading.Tasks;
using DSharpPlus.SlashCommands;
using System.Net;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Http.Headers;
using System.Reflection;

namespace Tau
{
    public class Bot
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {

            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = HiddenToken.myToken,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged
            });

            var slash = discord.UseSlashCommands();

            // clear command list with PUT request
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("PUT"), "https://discord.com/api/v10/applications/1050971170448625695/commands"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Bot " + HiddenToken.myToken);

                    request.Content = new StringContent("[]");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(response);
                    Console.ResetColor();
                }
            }
#if DEBUG
            // test guild

            var asm = Assembly.GetExecutingAssembly();
            slash.RegisterCommands(asm, 1050971027632558110);
#else
            // global
            slash.RegisterCommands(asm);
#endif

            await discord.ConnectAsync();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Bot online!");
            Console.ResetColor();

            await Task.Delay(-1);
        }
    }
}