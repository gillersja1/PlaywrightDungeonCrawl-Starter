using Microsoft.Playwright.NUnit;

namespace DungeonCrawler.Tests;

/// <summary>
/// Base class for your dungeon tests. Inherit from it and you get:
/// <list type="bullet">
///   <item><c>Page</c>: a fresh browser tab for every test</item>
///   <item><c>Expect(...)</c>: Playwright's assertions, which wait and retry</item>
///   <item><c>EnterAsync</c> and <c>TakeTreasureAsync</c>: helpers for the dungeon</item>
/// </list>
/// </summary>
public abstract class DungeonTest : PageTest
{
    /// <summary>
    /// The dungeon's address, from dungeon.runsettings. The DUNGEON_URL
    /// environment variable overrides it.
    /// </summary>
    protected static string DungeonUrl =>
        (Environment.GetEnvironmentVariable("DUNGEON_URL")
            ?? TestContext.Parameters.Get("DungeonUrl", "http://localhost:5299")).TrimEnd('/');

    /// <summary>Opens a page in the dungeon, e.g. <c>EnterAsync("/rooms/welcome-mat")</c>.</summary>
    protected async Task EnterAsync(string path)
    {
        try
        {
            await Page.GotoAsync(DungeonUrl + path);
        }
        catch (PlaywrightException ex) when (ex.Message.Contains("ERR_CONNECTION_REFUSED"))
        {
            Assert.Fail($"Can't reach the dungeon at {DungeonUrl}. Is it running? " +
                        "The address is set in dungeon.runsettings.");
        }
    }

    /// <summary>
    /// Waits for a cleared room's treasure flag, writes it to the test output
    /// (Test Explorer shows it), and returns it.
    /// </summary>
    protected async Task<string> TakeTreasureAsync()
    {
        var flag = Page.GetByLabel("Treasure flag");
        await Expect(flag).ToContainTextAsync("PDC{");
        var text = (await flag.TextContentAsync())!.Trim();
        TestContext.Out.WriteLine($"Treasure flag: {text}");
        return text;
    }
}
