# Playwright Dungeon Crawl: Starter Kit

Your test project for **Playwright Dungeon Crawl**, a dungeon you clear by
writing [Playwright for .NET](https://playwright.dev/dotnet/) tests in C#.

No experience needed. Floor 1 of the dungeon walks you through everything
below step by step, then has you write your first test.

## Get going

1. Install **Visual Studio Community** (free) with the **.NET desktop
   development** workload.
2. In Visual Studio, choose **Clone a repository** and paste this repo's URL.
3. Open **DungeonCrawler.slnx**.
4. Check that `DungeonCrawler.Tests/dungeon.runsettings` points at the
   dungeon (the `DungeonUrl` line).
5. Open **Test › Test Explorer** and click **Run All Tests**. The first run
   downloads a browser for Playwright, which takes about a minute.

When `TheDungeonIsOpen` goes green, head to floor 1 of the dungeon.

## What's in here

| File | What it's for |
|---|---|
| `DungeonTest.cs` | Base class for your tests: a browser tab (`Page`), assertions (`Expect`), and dungeon helpers |
| `ExampleTests.cs` | One working test to copy from |
| `dungeon.runsettings` | Where the dungeon is, and which browser to use |
| `BrowserInstall.cs` | Downloads Playwright's browser on the first run, so you don't have to |

Add one test class per trap room, next to `ExampleTests.cs`.

## Helpers

```csharp
await EnterAsync("/rooms/welcome-mat");   // open a page in the dungeon
var flag = await TakeTreasureAsync();     // wait for the flag, print it, return it
```

## Watching a test run

Tests are fast, often too fast to see. Two ways to slow them down:

- **Step through with the Playwright Inspector.** Add `await Page.PauseAsync();`
  where you want to stop. The browser opens with the Inspector next to it:
  step one line at a time, resume, or use **Pick locator** to hover over the
  page and get a locator for any element. Remove the line when you're done,
  or the test waits for you forever.
- **Slow motion.** Turn on `SlowMo` in `dungeon.runsettings` to wait after
  every action. Turn it off for timed trap rooms.

## Running from a terminal

```bash
dotnet test
```

The browser window opens so you can watch. To run without it, add
`-- Playwright.LaunchOptions.Headless=true`. To point at a different copy of
the dungeon without editing files, set the `DUNGEON_URL` environment
variable.
