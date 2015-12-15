using NUnit.Framework;
using System;

namespace TrelloWrapper.Test
{
    [TestFixture]
    public class MoverCartaoEmInvestigacao : TesteComCenario
    {
        private Cartao cartao;
        private TrelloConnection treller;

        [OneTimeSetUp]
        public void Cenario()
        {
            treller = new TrelloConnection();

            cartao = new Cartao
            {
                Nome = "GSOL1",
                Severidade = NivelSeveridade.Alta,
                Sistema = "S160",
                DataSubmissao = DateTime.Now
            };

            treller.CadastraCartao(cartao);
        }

        [Test]
        public void PossoMoverParaEmResolucao()
        {
            treller.MoveParaEmResolucao(cartao);

            Assert.That(cartao.Lista, Is.EqualTo(ListaEstado.Em_Resolucao));
        }

        [Test]
        public void PossoMoverParaPendencia()
        {
            treller.MoveParaPendencia(cartao);

            Assert.That(cartao.Lista, Is.EqualTo(ListaEstado.Pendencia));
        }
    }
}
