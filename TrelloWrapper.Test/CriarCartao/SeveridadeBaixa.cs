using NUnit.Framework;
using System;

namespace TrelloWrapper.Test.CriarCartao
{
    [TestFixture]
    public class SeveridadeBaixa : TesteComCenario
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
                Severidade = NivelSeveridade.Baixa,
                Sistema = "S160",
                DataSubmissao = DateTime.Now
            };

            treller.CadastraCartao(cartao);
        }

        [Test]
        public void DevePossuirSeveridadeBaixa()
        {
            Assert.That(cartao.Severidade, Is.EqualTo(NivelSeveridade.Baixa));
        }

        [Test]
        public void DeveSerPostoEmInvestigacaoEm48Horas()
        {
            Assert.That(cartao.PrazoFinalizacao, Is.EqualTo(cartao.DataSubmissao.AddHours(48)));
        }
    }
}
