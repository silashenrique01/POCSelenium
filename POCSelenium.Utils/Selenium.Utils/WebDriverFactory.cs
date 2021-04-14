using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using Selenium.Utils;

namespace POCSelenium.Utils.Selenium.Utils
{
    public static class WebDriverFactory
    {

        public static IWebDriver CreateWebDriver(Browser browser, string pathDriver)
        {
            IWebDriver webDriver = null;

            switch (browser)
            {
                case Browser.Edge:
                    EdgeDriverService service = EdgeDriverService.CreateDefaultService(pathDriver);
                    webDriver = new EdgeDriver(service);
                    break;
                case Browser.Chrome:
                    webDriver = new ChromeDriver(pathDriver);
                    break;
            }
            return webDriver;
        }
    }
}
