using NUnit.Framework;
using System;
using System.Linq;
using TrelloNet;

namespace TrelloWrapper.Test.Integrados.CriarCartao
{
    [TestFixture]
    public class SeveridadeBaixa : TesteComCenario
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
                Nome = "SEVERIDADE_BAIXA",
                Severidade = NivelSeveridade.Baixa,
                DataSubmissao = DateTime.Now
            };

            quadro.AdicionaCartaoA(cartao, quadro.Submitted);
        }

        [Test]
        public void DevePossuirSeveridadeBaixa()
        {
            var card = trello.Cards.Search("SEVERIDADE_BAIXA").SingleOrDefault();

            Assert.That(card.LabelColors, Contains.Item(Color.Blue));
        }

        [Test]
        public void DeveSerPostoEmInvestigacaoEm48Horas()
        {
            var card = trello.Cards.Search("SEVERIDADE_BAIXA").SingleOrDefault();
            var prazo = card.Due.Value.Subtract(cartao.DataSubmissao).Hours;

            Assert.That(prazo, Is.EqualTo(48));
        }
    }
}
