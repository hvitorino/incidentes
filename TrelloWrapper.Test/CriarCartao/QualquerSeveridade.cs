using NUnit.Framework;
using System;
using System.Linq;
using TrelloNet;

namespace TrelloWrapper.Test.CriarCartao
{
    [TestFixture]
    public class QualquerSeveridade
    {
        private const string chave = "950a4f35f078102cc55be50178a55181";
        private const string token = "6b7d13c413e8d89cd85e27f1e094f10d879f44c5f18b27f8c594d4cd9b051e9c";

        private Cartao cartao;
        private Incidente incidente;
        private Treller treller;

        [OneTimeSetUp]
        public void Cenario()
        {
            treller = new Treller();

            incidente = new Incidente
            {
                Id = "QUALQUER_SEVERIDADE",
                Severidade = NivelSeveridade.Baixa,
                Sistema = "S160",
                DataSubmissao = DateTime.Now
            };

            cartao = treller.cadastrarIncidente(incidente);
        }

        [OneTimeTearDown]
        public void DesfazCenario()
        {
            var trello = new Trello(chave);
            trello.Authorize(token);

            var quadroS160 = trello.Organizations.WithId("s160");

            var incidentes = trello.Boards.ForOrganization(quadroS160)
                .Where(board => board.Name.Equals("incidentes", StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            var allCards = trello.Cards.ForBoard(incidentes);

            foreach (var card in allCards)
            {
                trello.Cards.Delete(card);
            }
        }

        [Test]
        public void DevePossuirOIdCurtoDoTrello()
        {
            Assert.That(cartao.ShortIdTrello, Is.GreaterThan(0));
        }

        [Test]
        public void DevePossuirEtiquetaNovoSLA()
        {
            Assert.That(cartao.EstadoSLA, Is.EqualTo(SLA.Novo));
        }

        [Test]
        public void DevePossuirNomeNoFormatoIdSeveridade()
        {
            Assert.That(cartao.Nome, Is.EqualTo(incidente.Id + " - " + incidente.Severidade));
        }

        [Test]
        public void DeveSerCriadoNaListaSubmitted()
        {
            Assert.That(cartao.Lista, Is.EqualTo(ListaEstado.Submitted));
        }
    }
}
