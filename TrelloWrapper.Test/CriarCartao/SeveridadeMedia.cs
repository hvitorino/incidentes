using NUnit.Framework;
using System;

namespace TrelloWrapper.Test.CriarCartao
{
    [TestFixture]
    class SeveridadeMedia
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
                Id = "SEVERIDADE_ALTA",
                Severidade = NivelSeveridade.Media,
                Sistema = "S160",
                DataSubmissao = DateTime.Now
            };

            cartao = treller.cadastrarIncidente(incidente);
        }

        [Test]
        public void DevePossuirSeveridadeMedia()
        {
            Assert.That(cartao.Severidade, Is.EqualTo(NivelSeveridade.Media));
        }

        [Test]
        public void DeveSerPostoEmInvestigacaoEm5Horas()
        {
            Assert.That(cartao.PrazoFinalizacao, Is.EqualTo(incidente.DataSubmissao.AddHours(5)));
        }
    }
}
