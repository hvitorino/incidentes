using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TrelloNet;

namespace TrelloWrapper.Test
{
    [TestFixture]
    public class CriarCartao
    {
        private const string chave = "950a4f35f078102cc55be50178a55181";
        private const string token = "6b7d13c413e8d89cd85e27f1e094f10d879f44c5f18b27f8c594d4cd9b051e9c";

        private Trello trello;

        private string idGSOL = "GSOL0001";
        private string severidade = "Alta";
        private DateTime dataSubmissao = DateTime.Now;

        public CriarCartao()
        {
            trello = new Trello(chave);
            trello.Authorize(token);

            var s160 = trello.Organizations.WithId("s160");

            var incidentes = trello.Boards.ForOrganization(s160)
                .Where(board => board.Name.Equals("incidentes", StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            var listaSubmitted = trello.Lists.ForBoard(new BoardId(incidentes.GetBoardId()))
                .Where(lista => lista.Name.Equals("Submitted", StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            var novoCartao = new NewCard(idGSOL + " - " + severidade, new ListId(listaSubmitted.Id));
            var cartaoCriado = trello.Cards.Add(novoCartao);

            trello.Cards.AddLabel(cartaoCriado, Color.Green);
            trello.Cards.ChangeDueDate(cartaoCriado, dataSubmissao.AddHours(5));
        }

        [Test]
        public void DeveTerNomeESeveridadeNoTitulo()
        {
        }
    }
}
