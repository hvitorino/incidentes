using NUnit.Framework;
using System;
using System.Linq;
using TrelloNet;

namespace TrelloWrapper.Test
{
    [TestFixture]
    public class MoverCartaoEmInvestigacao : TesteComCenario
    {
        private Cartao cartao;
        private TrelloConnection trelloConnection;
        private Trello trelloAPI;
        private Quadro quadro;

        [OneTimeSetUp]
        public void Cenario()
        {
            trelloConnection = new TrelloConnection();

            quadro = new Quadro(trelloConnection);

            cartao = new Cartao
            {
                Nome = "GSOL1",
                Severidade = NivelSeveridade.Alta,
                Sistema = "S160",
                DataSubmissao = DateTime.Now
            };

            quadro.AdicionaCartaoA(cartao, quadro.Submitted);
        }

        [Test]
        public void PossoMoverParaEmResolucao()
        {
            trelloConnection.MoveParaEmResolucao(quadro, cartao);

            var emResolucao = trelloAPI.Boards.Search("Em_Resolucao").SingleOrDefault();
            var cartaoTrello = trelloAPI.Cards.Search("GSOL1").SingleOrDefault();

            Assert.That(cartaoTrello, Is.Not.Null);
            Assert.That(cartaoTrello.IdList, Is.EqualTo(emResolucao.GetBoardId()));
        }

        [Test]
        public void PossoMoverParaPendencia()
        {
            trelloConnection.MoveParaPendencia(quadro, cartao);

            var pendencia = trelloAPI.Boards.Search("Pendencia").SingleOrDefault();
            var cartaoTrello = trelloAPI.Cards.Search("GSOL1").SingleOrDefault();

            Assert.That(cartaoTrello, Is.Not.Null);
            Assert.That(cartaoTrello.IdList, Is.EqualTo(pendencia.GetBoardId()));
        }
    }
}
