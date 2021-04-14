using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Selenium.Utils;
using System.IO;

namespace POCTest.CadastroUsuario.Tests
{
    [TestClass]
    public class CadastroUsuarioTest
    {
        private readonly IConfiguration _configuration;
        public CadastroUsuarioTest()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile($"appsettings.Development.json");
            _configuration = builder.Build();
        }

        [TestMethod]
        public void CadastrarUsuario(Browser browser, string nomeInserido, string nomeRecebido)
        {
            TelaCadastroUsuarioTest tela = new TelaCadastroUsuarioTest(_configuration, browser);

            tela.CarregarPagina();
            tela.InserirNome(nomeInserido);
            string result = tela.ObterNome();
            tela.Fechar();
            Assert.AreEqual("", result);
        }


    }
}
