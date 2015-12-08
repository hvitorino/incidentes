using NUnit.Framework;
using System;

namespace TrelloWrapper.Test.CriarCartao
{
    [TestFixture]
    public class SeveridadeAlta
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
                Severidade = NivelSeveridade.Alta,
                Sistema = "S160",
                DataSubmissao = DateTime.Now
            };

            cartao = treller.cadastrarIncidente(incidente);
        }

        [Test]
        public void DevePossuirSeveridadeAlta()
        {
            Assert.That(cartao.Severidade, Is.EqualTo(NivelSeveridade.Alta));
        }

        [Test]
        public void DeveSerPostoEmInvestigacaoEm2Horas()
        {
            Assert.That(cartao.PrazoFinalizacao, Is.EqualTo(incidente.DataSubmissao.AddHours(2)));
        }
    }
}
