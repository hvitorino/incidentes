using System;
using System.Linq;
using TrelloNet;

namespace TrelloWrapper
{
    public class Treller
    {
        private const string chave = "950a4f35f078102cc55be50178a55181";
        private const string token = "6b7d13c413e8d89cd85e27f1e094f10d879f44c5f18b27f8c594d4cd9b051e9c";

        private Trello trello;

        public Cartao cadastrarIncidente(Incidente incidente)
        {
            trello = new Trello(chave);
            trello.Authorize(token);

            var equipeSistema = trello.Organizations.WithId(incidente.Sistema.ToLower());

            var incidentes = trello.Boards.ForOrganization(equipeSistema)
                .Where(board => board.Name.Equals("incidentes", StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            var listaSubmitted = trello.Lists.ForBoard(new BoardId(incidentes.GetBoardId()))
                .Where(lista => lista.Name.Equals("Submitted", StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            var novoCartao = new NewCard(incidente.Id + " - " + incidente.Severidade, new ListId(listaSubmitted.Id));
            var cartaoCriado = trello.Cards.Add(novoCartao);

            trello.Cards.AddLabel(cartaoCriado, Color.Red);
            trello.Cards.AddLabel(cartaoCriado, Color.Green);
            trello.Cards.ChangeDueDate(cartaoCriado, incidente.DataSubmissao);

            return new Cartao
            {
                EstadoSLA = SLA.Novo,
                Lista = ListaEstado.Submitted,
                Nome = incidente.Id + " - " + incidente.Severidade
            };
        }
    }
}