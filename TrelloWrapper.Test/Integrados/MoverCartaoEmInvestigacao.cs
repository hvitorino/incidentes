using NUnit.Framework;
using System;
using System.Linq;
using TrelloNet;

namespace TrelloWrapper.Test.Integrados
{
    [TestFixture]
    public class MoverCartaoEmInvestigacao : TesteComCenario
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

            quadro.AdicionaCartaoA(cartao, quadro.EmInvestigacao);
        }

        [Test]
        public void PossoMoverParaEmResolucao()
        {
            quadro.MoveCartaoParaEmResolucao(cartao);

            var emResolucao = trello.Boards.Search(quadro.Nome).SingleOrDefault();
            var cartaoTrello = trello.Cards.Search("GSOL1").SingleOrDefault();

            Assert.That(cartaoTrello, Is.Not.Null);
            Assert.That(cartaoTrello.IdList, Is.EqualTo(emResolucao.GetBoardId()));
        }

        [Test]
        public void PossoMoverParaPendencia()
        {
            quadro.MoveCartaoParaPendencia(cartao);

            var pendencia = trello.Boards.Search("Pendencia").SingleOrDefault();
            var cartaoTrello = trello.Cards.Search("GSOL1").SingleOrDefault();

            Assert.That(cartaoTrello, Is.Not.Null);
            Assert.That(cartaoTrello.IdList, Is.EqualTo(pendencia.GetBoardId()));
        }
    }
}
