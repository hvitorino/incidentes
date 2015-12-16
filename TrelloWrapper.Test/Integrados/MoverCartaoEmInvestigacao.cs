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

            var emResolucao = TrelloHelper.RecuperarLista(quadro.EmResolucao);
            var cartaoTrello = TrelloHelper.RecuperarCartao(cartao);

            Assert.That(cartaoTrello, Is.Not.Null);
            Assert.That(cartaoTrello.IdList, Is.EqualTo(emResolucao.Id));
        }

        [Test]
        public void PossoMoverParaPendencia()
        {
            quadro.MoveCartaoParaPendencia(cartao);

            var pendencia = TrelloHelper.RecuperarLista(quadro.Pendencia);
            var cartaoTrello = TrelloHelper.RecuperarCartao(cartao);

            Assert.That(cartaoTrello, Is.Not.Null);
            Assert.That(cartaoTrello.IdList, Is.EqualTo(pendencia.GetListId()));
        }
    }
}
