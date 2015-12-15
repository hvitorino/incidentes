using NUnit.Framework;
using System;
using System.Linq;
using TrelloNet;

namespace TrelloWrapper.Test.Integrados.CriarCartao
{
    [TestFixture]
    public class QualquerSeveridade : TesteComCenario
    {
        private Cartao cartao;
        private Quadro quadro;
        private Trello trello;

        [OneTimeSetUp]
        public void Cenario()
        {
            trello = TrelloFactory.API;

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
            var card = trello.Cards.Search("QUALQUER_SEVERIDADE").SingleOrDefault();

            Assert.That(card, Is.Not.Null);
        }

        [Test]
        public void DevePossuirNome()
        {
            var card = trello.Cards.Search("QUALQUER_SEVERIDADE").SingleOrDefault();

            Assert.That(card.Name, Is.Not.Null);
        }

        [Test]
        public void DeveSerCriadoNaListaSubmitted()
        {
            var quadro = trello.Boards.Search(cartao.Nome).SingleOrDefault();
            var listaSubmitted = trello.Lists.ForBoard(new BoardId(quadro.Id)).SingleOrDefault();
            var card = trello.Cards.Search("QUALQUER_SEVERIDADE").SingleOrDefault();

            Assert.That(card.IdBoard, Is.EqualTo(listaSubmitted.Id));
        }
    }
}
