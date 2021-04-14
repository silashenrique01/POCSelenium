using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using POCSelenium.Utils.Selenium.Utils;
using Selenium.Utils;
using System;

namespace POCSelenium.Tests
{
    public class TelaCadastroUsuarioTest
    {
        private readonly IConfiguration _configuration;
        private readonly Browser _browser;
        private readonly IWebDriver _webDriver;

        public TelaCadastroUsuarioTest(IConfiguration configuration, Browser browser)
        {
            _browser = browser;
            _configuration = configuration;


            string caminhoDriver = null;

            if (browser == Browser.Edge)
            {
                caminhoDriver = _configuration.GetSection("Selenium:CaminhoDriverEdge").Value;
            }
            else if (browser == Browser.Chrome)
            {
                caminhoDriver = _configuration.GetSection("Selenium:CaminhoDriverChrome").Value;
            }

            _webDriver = WebDriverFactory.CreateWebDriver(browser, caminhoDriver);
        }

        public void CarregarPagina()
        {
            _webDriver.Submit(By.Id("btnCadastrarUsuario"));

            WebDriverWait webDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            webDriverWait.Until((d) => d.FindElement(By.Id("nome")) != null);
        }

        public void InserirNome(string nome)
        {
            _webDriver.SetText(By.Name("name"), nome);
        }

        public string ObterNome()
        {
            return _webDriver.GetText(By.Id("nome"));
        }
        public void Fechar()
        {
            _webDriver.Quit();
        }

    }
}
