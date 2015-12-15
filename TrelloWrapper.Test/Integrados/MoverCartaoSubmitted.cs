using NUnit.Framework;
using System;
using TrelloNet;

namespace TrelloWrapper.Test.Integrados
{
    [TestFixture]
    public class MoverCartaoSubmitted : TesteComCenario
    {
        private Cartao cartao;
        private Quadro quadro;
        private Trello trelloAPI;

        [OneTimeSetUp]
        public void Cenario()
        {
            trelloAPI = TrelloFactory.API;

            quadro = new Quadro("S160", new TrelloConnection());

            cartao = new Cartao
            {
                Nome = "GSOL1",
                Severidade = NivelSeveridade.Alta,
                DataSubmissao = DateTime.Now
            };

            quadro.AdicionaCartaoA(cartao, quadro.Submitted);
        }

        [Test]
        public void PossoMoverParaEmInvestigacao()
        {
            quadro.MoveCartaoParaEmInvestigacao(cartao);
        }
    }
}
