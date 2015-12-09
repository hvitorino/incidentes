using NUnit.Framework;
using System;

namespace TrelloWrapper.Test.CriarCartao
{
    [TestFixture]
    public class SeveridadeBaixa : TesteComCenario
    {
        private Cartao cartao;
        private Incidente incidente;
        private Treller treller;

        [OneTimeSetUp]
        public void Cenario()
        {
            treller = new Treller();

            incidente = new Incidente
            {
                Id = "SEVERIDADE_BAIXA",
                Severidade = NivelSeveridade.Baixa,
                Sistema = "S160",
                DataSubmissao = DateTime.Now
            };

            cartao = treller.cadastrarIncidente(incidente);
        }

        [Test]
        public void DevePossuirSeveridadeBaixa()
        {
            Assert.That(cartao.Severidade, Is.EqualTo(NivelSeveridade.Baixa));
        }

        [Test]
        public void DeveSerPostoEmInvestigacaoEm48Horas()
        {
            Assert.That(cartao.PrazoFinalizacao, Is.EqualTo(incidente.DataSubmissao.AddHours(48)));
        }
    }
}
