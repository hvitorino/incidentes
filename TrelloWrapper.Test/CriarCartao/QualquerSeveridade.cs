using NUnit.Framework;
using System;

namespace TrelloWrapper.Test.CriarCartao
{
    [TestFixture]
    public class QualquerSeveridade : TesteComCenario
    {
        private Cartao cartao;
        private TrelloConnection treller;

        [OneTimeSetUp]
        public void Cenario()
        {
            treller = new TrelloConnection();

            cartao = new Cartao
            {
                Nome = "QUALQUER_SEVERIDADE",
                Severidade = NivelSeveridade.Baixa,
                Sistema = "S160",
                DataSubmissao = DateTime.Now
            };

            treller.CadastraCartao(cartao);
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
        public void DeveSerCriadoNaListaSubmitted()
        {
            Assert.That(cartao.Lista, Is.EqualTo(ListaEstado.Submitted));
        }
    }
}
