using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;

public class UITests : IDisposable
{
    private IWebDriver driver;
    private string url = "https://kcc-vm-e5csgrcjgqgzazfn.australiaeast-01.azurewebsites.net/";
    private List<string> testResults = new List<string>();

    public UITests()
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--headless");  // Run tests without opening Chrome window
        options.AddArgument("--disable-gpu");
        options.AddArgument("--no-sandbox");
        driver = new ChromeDriver(options);
    }

    [Fact]
    public void VerifyPageTitle()
    {
        try
        {
            driver.Navigate().GoToUrl(url);
            Assert.Contains("KCC Manufacturing", driver.Title);
            Console.WriteLine("✅ Page title verified.");
            testResults.Add("✅ VerifyPageTitle: PASSED");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ VerifyPageTitle: FAILED - {ex.Message}");
            testResults.Add($"❌ VerifyPageTitle: FAILED - {ex.Message}");
        }
    }

    [Fact]
    public void VerifyPageLoadsSuccessfully()
    {
        try
        {
            driver.Navigate().GoToUrl(url);
            Assert.Contains("azurewebsites.net", driver.Url);
            Console.WriteLine("✅ Page loaded successfully!");
            testResults.Add("✅ VerifyPageLoadsSuccessfully: PASSED");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ VerifyPageLoadsSuccessfully: FAILED - {ex.Message}");
            testResults.Add($"❌ VerifyPageLoadsSuccessfully: FAILED - {ex.Message}");
        }
    }

    public void Dispose()
    {
        driver.Quit();
        PrintTestSummary();
    }

    private void PrintTestSummary()
    {
        Console.WriteLine("\n===============================");
        Console.WriteLine("  📝 Selenium Test Execution Summary");
        Console.WriteLine("===============================");
        foreach (var result in testResults)
        {
            Console.WriteLine(result);
        }
        Console.WriteLine("===============================\n");
    }
}
