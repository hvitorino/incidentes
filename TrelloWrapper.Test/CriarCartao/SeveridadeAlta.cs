using NUnit.Framework;
using System;

namespace TrelloWrapper.Test.CriarCartao
{
    [TestFixture]
    public class SeveridadeAlta : TesteComCenario
    {
        private Cartao cartao;
        private TrelloConnection treller;

        [OneTimeSetUp]
        public void Cenario()
        {
            treller = new TrelloConnection();

            cartao = new Cartao
            {
                Nome = "GSOL1",
                Severidade = NivelSeveridade.Alta,
                Sistema = "S160",
                DataSubmissao = DateTime.Now
            };

            treller.CadastraCartao(cartao);
        }

        [Test]
        public void DevePossuirSeveridadeAlta()
        {
            Assert.That(cartao.Severidade, Is.EqualTo(NivelSeveridade.Alta));
        }

        [Test]
        public void DeveSerPostoEmInvestigacaoEm2Horas()
        {
            Assert.That(cartao.PrazoFinalizacao, Is.EqualTo(cartao.DataSubmissao.AddHours(2)));
        }
    }
}
