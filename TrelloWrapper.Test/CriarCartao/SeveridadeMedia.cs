using NUnit.Framework;
using System;

namespace TrelloWrapper.Test.CriarCartao
{
    [TestFixture]
    public class SeveridadeMedia : TesteComCenario
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
                Severidade = NivelSeveridade.Media,
                Sistema = "S160",
                DataSubmissao = DateTime.Now
            };

            treller.CadastraCartao(cartao);
        }

        [Test]
        public void DevePossuirSeveridadeMedia()
        {
            Assert.That(cartao.Severidade, Is.EqualTo(NivelSeveridade.Media));
        }

        [Test]
        public void DeveSerPostoEmInvestigacaoEm5Horas()
        {
            Assert.That(cartao.PrazoFinalizacao, Is.EqualTo(cartao.DataSubmissao.AddHours(5)));
        }
    }
}
