using NUnit.Framework;
using System;

namespace TrelloWrapper.Test.Integrados.CriarCartao
{
    [TestFixture]
    public class QualquerSeveridade : TesteComCenario
    {
        private Cartao cartao;
        
        [OneTimeSetUp]
        public void Cenario()
        {
            quadro = new Quadro("S160", new TrelloConnection());

            cartao = new Cartao
            {
                Nome = "QUALQUER_SEVERIDADE",
                Severidade = NivelSeveridade.Baixa,
                DataSubmissao = DateTime.Now
            };

            quadro.AdicionaCartaoA(cartao, quadro.Submitted);
        }

        [Test]
        public void DeveSerCadastrado()
        {
            var card = TrelloHelper.RecuperaCartao(cartao);

            Assert.That(card, Is.Not.Null);
        }

        [Test]
        public void DevePossuirNome()
        {
            var card = TrelloHelper.RecuperaCartao(cartao);

            Assert.That(card.Name, Is.Not.Null);
        }

        [Test]
        public void DeveSerCriadoNaListaSubmitted()
        {
            var listaSubmitted = TrelloHelper.RecuperaLista(cartao.Lista);
            var card = TrelloHelper.RecuperaCartao(cartao);

            Assert.That(card.IdBoard, Is.EqualTo(listaSubmitted.IdBoard));
        }
    }
}
