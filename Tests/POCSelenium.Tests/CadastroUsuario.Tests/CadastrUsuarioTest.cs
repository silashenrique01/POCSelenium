using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Selenium.Utils;
using System.IO;

namespace POCSelenium.Tests.CadastroUsuario.Tests
{
    [TestClass]
    public class CadastrUsuarioTest
    {
        private readonly IConfiguration _configuration;
        public CadastrUsuarioTest()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile($"appsettings.json");
            _configuration = builder.Build();
        }

        [DataTestMethod]
        [DataRow(Browser.Edge, "Silas Henrique", "Silas Henrique")]
        [DataRow(Browser.Chrome, "Rafael Cardoso", "Rafael Cardoso")]
        public void CadastrarUsuario(Browser browser, string nomeInserido, string nomeRecebido)
        {
            TelaCadastroUsuarioTest tela = new TelaCadastroUsuarioTest(_configuration, browser);

            tela.CarregarPagina();
            tela.InserirNome(nomeInserido);
            var result = tela.ObterNome();
            tela.Fechar();

            Assert.AreEqual(nomeInserido, result);
        }


    }
}
