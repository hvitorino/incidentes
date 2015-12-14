using NUnit.Framework;
using System;

namespace TrelloWrapper.Test.CriarCartao
{
    [TestFixture]
    public class QualquerSeveridade : TesteComCenario
    {
        private Cartao cartao;
        private Incidente incidente;
        private TrelloConnection treller;

        [OneTimeSetUp]
        public void Cenario()
        {
            treller = new TrelloConnection();

            incidente = new Incidente
            {
                Id = "QUALQUER_SEVERIDADE",
                Severidade = NivelSeveridade.Baixa,
                Sistema = "S160",
                DataSubmissao = DateTime.Now
            };

            cartao = treller.cadastrarIncidente(incidente);
        }

        [Test]
        public void DevePossuirOIdCurtoDoTrello()
        {
            Assert.That(cartao.ShortIdTrello, Is.GreaterThan(0));
        }

        [Test]
        public void DevePossuirEtiquetaNovoSLA()
        {
            Assert.That(cartao.EstadoSLA, Is.EqualTo(SLA.Novo));
        }

        [Test]
        public void DevePossuirNomeNoFormatoIdSeveridade()
        {
            Assert.That(cartao.Nome, Is.EqualTo(incidente.Id + " - " + incidente.Severidade));
        }

        [Test]
        public void DeveSerCriadoNaListaSubmitted()
        {
            Assert.That(cartao.Lista, Is.EqualTo(ListaEstado.Submitted));
        }
    }
}
