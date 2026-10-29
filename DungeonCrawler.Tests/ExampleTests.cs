namespace DungeonCrawler.Tests;

/// <summary>
/// A working example to copy from. Add your own test classes next to this
/// file, one per trap room.
/// </summary>
public class ExampleTests : DungeonTest
{
    [Test]
    public async Task TheDungeonIsOpen()
    {
        // Open the dungeon's map page.
        await EnterAsync("/");

        // Check we landed in the right place. Expect(...) waits and retries,
        // so there's no need to sleep while the page loads.
        await Expect(Page).ToHaveTitleAsync(new Regex("Playwright Dungeon Crawl"));
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Level = 1 })).ToBeVisibleAsync();
    }

    /// <summary>
    /// The same test, paused so you can step through it in the Playwright
    /// Inspector. [Explicit] keeps it out of "Run All": right-click it in
    /// Test Explorer and choose Run.
    /// </summary>
    [Test, Explicit("Pauses for the Playwright Inspector; run it on its own.")]
    public async Task StepThroughWithTheInspector()
    {
        // Opens the Inspector and waits. Click "Step over" to run one line at
        // a time, or "Resume" to carry on.
        await Page.PauseAsync();

        await EnterAsync("/");
        await Expect(Page).ToHaveTitleAsync(new Regex("Playwright Dungeon Crawl"));
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Level = 1 })).ToBeVisibleAsync();
    }
}
