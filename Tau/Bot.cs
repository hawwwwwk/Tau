using DSharpPlus;
using DSharpPlus.SlashCommands;
using System.Net.Http.Headers;
using System.Reflection;
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
                Console.WriteLine("Is there a config file?");
                Console.ReadLine();
                System.Environment.Exit(1);
            }

            await UpdateCommands(ConfigInfo.ApplicationID, ConfigInfo.BotToken);
            await UpdateCommands(ConfigInfo.ApplicationID, ConfigInfo.BotToken, ConfigInfo.TestGuildID);

            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = ConfigInfo.BotToken,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged
            });
            var slash = discord.UseSlashCommands();

#if DEBUG
            // test guild
            var asm = Assembly.GetExecutingAssembly();
            slash.RegisterCommands(asm, Convert.ToUInt64(ConfigInfo.TestGuildID));
            Console.WriteLine("DEBUG ON!");
#else
            // global
            var asm = Assembly.GetExecutingAssembly();
            slash.RegisterCommands(asm);
#endif

            await discord.ConnectAsync();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Bot online!");
            Console.ResetColor();

            await Task.Delay(-1);
        }
        
        // Clears old commands from slashcommand handler.
        static async Task UpdateCommands(string? applicationID, string? botToken, string? testGuildID = null)
        {
            using var httpClient = new HttpClient();
            string url;
            if (testGuildID == null)
            {
                url = "https://discord.com/api/v10/applications/" + applicationID + "/commands";
            }
            else
            {
                url = "https://discord.com/api/v10/applications/" + applicationID + "/guilds/" + testGuildID + "/commands";
            }

            using var request = new HttpRequestMessage(new HttpMethod("PUT"), url);
            request.Headers.TryAddWithoutValidation("Authorization", "Bot " + botToken);

            request.Content = new StringContent("[]");
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            var response = await httpClient.SendAsync(request);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            if (testGuildID == null)
            {
                Console.WriteLine("Cleared Global Commands");
            }
            else
            {
                Console.WriteLine("Cleared Guild Commands");
            }
            Console.ResetColor();
        }
    }
}