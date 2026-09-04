using Microsoft.Playwright;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace ecommerce_regression_tests.Utils;

[Parallelizable(ParallelScope.Self)]
public abstract class TestBase
{
    protected IPlaywright? PlaywrightInstance;
    protected IBrowser? Browser;
    protected IPage? Page;
    protected IBrowserContext? Context;

    [SetUp]
    public async Task BaseSetUp()
    {
        PlaywrightInstance = await Playwright.CreateAsync();
        Browser = await PlaywrightInstance.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true,
            Args = new[] { "--no-sandbox" }
        });
        Context = await Browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize { Width = 1280, Height = 720 }
        });
        Page = await Context.NewPageAsync();
    }

    [TearDown]
    public async Task BaseTearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            var screenshotPath = Path.Combine(
                TestContext.CurrentContext.TestDirectory,
                "Screenshots",
                $"{TestContext.CurrentContext.Test.Name}.png"
            );
            Directory.CreateDirectory(Path.GetDirectoryName(screenshotPath)!);
            await Page!.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath, FullPage = true });
        }

        if (Context != null) await Context.CloseAsync();
        if (Browser != null) await Browser.CloseAsync();
        PlaywrightInstance?.Dispose();
    }
}
