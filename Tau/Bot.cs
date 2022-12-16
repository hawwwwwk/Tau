using DSharpPlus;
using System;
using System.Threading.Tasks;
using DSharpPlus.SlashCommands;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.IO;
using System.Text.Json;
using Tau.Config;

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
#if DEBUG
            string text = File.ReadAllText("./Config/TESTConfig.json");
#else
            string text = File.ReadAllText("./Config/Config.json");
#endif
            var ConfigInfo = JsonSerializer.Deserialize<config>(text);
            if (ConfigInfo == null)
            {
                Console.WriteLine("Is there a config file?"); // i only added this so it'd stop saying "wHaT iF iTs NuLl"
                Console.ReadLine();
                System.Environment.Exit(1);
            }
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = ConfigInfo.BotToken,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged
            });

            var slash = discord.UseSlashCommands();

            // clear command list with PUT request
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("PUT"), "https://discord.com/api/v10/applications/" + ConfigInfo.ApplicationID + "/commands"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Bot " + ConfigInfo.BotToken);

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
            slash.RegisterCommands(asm, Convert.ToUInt64(ConfigInfo.TestGuildID));
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
         static void ImportFromConfig()
        {
#if DEBUG
            string text = File.ReadAllText("@./TESTConfig.json");
#else
            string text = File.ReadAllText("@./Config.json");
#endif
            var ConfigInfo = JsonSerializer.Deserialize<config>(text);
        }
    }
}