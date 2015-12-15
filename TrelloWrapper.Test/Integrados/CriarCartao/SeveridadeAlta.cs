using NUnit.Framework;
using System;
using System.Linq;
using TrelloNet;

namespace TrelloWrapper.Test.Integrados.CriarCartao
{
    [TestFixture]
    public class SeveridadeAlta : TesteComCenario
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
                Nome = "SEVERIDADE_ALTA",
                Severidade = NivelSeveridade.Alta,
                DataSubmissao = DateTime.Now
            };

            quadro.AdicionaCartaoA(cartao, quadro.Submitted);
        }

        [Test]
        public void DevePossuirEtiquetaDeSeveridadeAlta()
        {
            var card = trello.Cards.Search("SEVERIDADE_ALTA").SingleOrDefault();

            Assert.That(card.LabelColors, Contains.Item(Color.Red));
        }

        [Test]
        public void DeveSerPostoEmInvestigacaoEm2Horas()
        {
            var card = trello.Cards.Search("SEVERIDADE_ALTA").SingleOrDefault();
            var prazo = card.Due.Value.Subtract(cartao.DataSubmissao).Hours;

            Assert.That(prazo, Is.EqualTo(2));
        }
    }
}
