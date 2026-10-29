namespace DungeonCrawler.Tests;

/// <summary>
/// Runs once before any test. Playwright needs its own copy of Chromium;
/// the first run downloads it (this can take a minute), later runs skip it.
/// </summary>
[SetUpFixture]
public class BrowserInstall
{
    [OneTimeSetUp]
    public void InstallChromium()
    {
        var exitCode = Microsoft.Playwright.Program.Main(new[] { "install", "chromium" });
        if (exitCode != 0)
        {
            throw new InvalidOperationException($"Playwright couldn't install Chromium (exit code {exitCode}).");
        }
    }
}
