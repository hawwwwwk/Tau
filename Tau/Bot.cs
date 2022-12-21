using DSharpPlus;
using DSharpPlus.SlashCommands;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using Tau.Config;

public class Bot
{
    public static DateTime startTime = DateTime.Now;
    public async Task RunAsync()
    {
#if DEBUG
        string text = File.ReadAllText("./Config/TESTConfig.json");
#else
            string text = File.ReadAllText("./Config/Config.json");
#endif
        var ConfigInfo = JsonSerializer.Deserialize<config>(text);

        if (ConfigInfo == null)
        {
            Console.WriteLine("Is there a config file? Program exiting now.");
            Console.ReadLine();
            Environment.Exit(1);
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

    // Clears old commands from slashcommand handler. I love rate limits!!!
    static async Task UpdateCommands(string? applicationID, string? botToken, string? testGuildID = null)
    {
        using var client = new HttpClient();
        var url = $"https://discord.com/api/v10/applications/{applicationID}{(testGuildID == null ? "/commands" : $"/guilds/{testGuildID}/commands")}";
        var request = new HttpRequestMessage(HttpMethod.Put, url)
        {
            Headers = { { "Authorization", $"Bot {botToken}" } },
            Content = new StringContent("[]", Encoding.UTF8, "application/json")
        };
        await client.SendAsync(request);
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(testGuildID == null ? "Cleared Global Commands" : "Cleared Guild Commands");
        Console.ResetColor();
    }
}
