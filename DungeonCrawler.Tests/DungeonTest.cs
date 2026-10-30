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
            ?? TestContext.Parameters.Get("DungeonUrl", "https://playwright-dungeon-crawl.onrender.com")).TrimEnd('/');

    /// <summary>Opens a page in the dungeon, e.g. <c>EnterAsync("/rooms/welcome-matt")</c>.</summary>
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
    /// (Test Explorer shows it), and returns it. Then holds the browser open
    /// for VictoryPauseSeconds (dungeon.runsettings) so you can see it worked.
    /// </summary>
    protected async Task<string> TakeTreasureAsync()
    {
        var flag = Page.GetByLabel("Treasure flag");
        await Expect(flag).ToContainTextAsync("PDC{");
        var text = (await flag.TextContentAsync())!.Trim();
        TestContext.Out.WriteLine($"Treasure flag: {text}");

        // A CI server has no one watching, so it skips the pause.
        var pause = float.TryParse(TestContext.Parameters.Get("VictoryPauseSeconds", "0"), out var seconds) ? seconds : 0;
        if (pause > 0 && Environment.GetEnvironmentVariable("CI") is null)
        {
            await flag.ScrollIntoViewIfNeededAsync();
            await Page.WaitForTimeoutAsync(pause * 1000);
        }
        return text;
    }
}
