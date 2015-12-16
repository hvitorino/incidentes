using NUnit.Framework;
using System;
using TrelloNet;

namespace TrelloWrapper.Test.Integrados
{
    [TestFixture]
    public class MoverCartaoSubmitted : TesteComCenario
    {
        private Cartao cartao;

        [OneTimeSetUp]
        public void Cenario()
        {
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

            var card = TrelloHelper.RecuperarCartao(cartao);
            var listaEmInvestigacao = TrelloHelper.RecuperarLista(cartao.Lista);

            Assert.That(card.IdBoard, Is.EqualTo(listaEmInvestigacao.IdBoard));
        }
    }
}
