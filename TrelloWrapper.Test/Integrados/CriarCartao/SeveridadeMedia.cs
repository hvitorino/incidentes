using NUnit.Framework;
using System;
using System.Linq;
using TrelloNet;

namespace TrelloWrapper.Test.Integrados.CriarCartao
{
    [TestFixture]
    public class SeveridadeMedia : TesteComCenario
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
                Nome = "SEVERIDADE_MEDIA",
                Severidade = NivelSeveridade.Media,
                DataSubmissao = DateTime.Now
            };

            quadro.AdicionaCartaoA(cartao, quadro.Submitted);
        }

        [Test]
        public void DevePossuirSeveridadeMedia()
        {
            var card = trello.Cards.Search("SEVERIDADE_MEDIA").SingleOrDefault();

            Assert.That(card.LabelColors, Contains.Item(Color.Orange));
        }

        [Test]
        public void DeveSerPostoEmInvestigacaoEm5Horas()
        {
            var card = trello.Cards.Search("SEVERIDADE_MEDIA").SingleOrDefault();
            var prazo = card.Due.Value.Subtract(cartao.DataSubmissao).Hours;

            Assert.That(prazo, Is.EqualTo(5));
        }
    }
}
