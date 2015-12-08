using NUnit.Framework;
using System;

namespace TrelloWrapper.Test
{
    [TestFixture]
    public class MoverCartaoSubmitted
    {
        private Cartao cartao;
        private Incidente incidente;
        private Treller treller;

        [OneTimeSetUp]
        public void Cenario()
        {
            treller = new Treller();

            incidente = new Incidente
            {
                Id = "EM_INVESTIGACAO_MOVIDO",
                Severidade = NivelSeveridade.Alta,
                Sistema = "S160",
                DataSubmissao = DateTime.Now
            };

            cartao = treller.cadastrarIncidente(incidente);

            treller.moverParaEmInvestigacao(cartao);
        }

        [Test]
        public void PossoMoverParaEmInvestigacao()
        {
            Assert.That(cartao.Lista, Is.EqualTo(ListaEstado.Em_Investigacao));
        }
    }
}
