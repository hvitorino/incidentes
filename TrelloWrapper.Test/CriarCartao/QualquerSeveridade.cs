using NUnit.Framework;
using System;

namespace TrelloWrapper.Test.CriarCartao
{
    [TestFixture]
    public class QualquerSeveridade
    {
        private Cartao cartao;
        private Incidente incidente;
        private Treller treller;

        [TestFixtureSetUp]
        public void Cenario()
        {
            treller = new Treller();

            incidente = new Incidente
            {
                Id = "GSOL00000001",
                Severidade = NivelSeveridade.Alta,
                Sistema = "S160",
                DataSubmissao = DateTime.Now
            };

            cartao = treller.cadastrarIncidente(incidente);
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
