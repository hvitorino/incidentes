using NUnit.Framework;
using System;

namespace TrelloWrapper.Test
{
    [TestFixture]
    public class MoverCartaoEmResolucao : TesteComCenario
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
        }

        [Test]
        public void PossoMoverParaPendencia()
        {
            treller.moverParaPendencia(cartao);

            Assert.That(cartao.Lista, Is.EqualTo(ListaEstado.Pendencia));
        }
    }
}
