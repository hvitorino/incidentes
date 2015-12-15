using System;
using System.Linq;
using TrelloNet;

namespace TrelloWrapper
{
    public static class TrelloExtensions
    {
        public static void excluirIncidentes(this Trello trello, string sistema)
        {
            var time = trello.Organizations.WithId(sistema.ToLower());

            var incidentes = trello.Boards.ForOrganization(time)
                .Where(board => board.Name.Equals("incidentes", StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            var allCards = trello.Cards.ForBoard(incidentes);

            foreach (var card in allCards)
            {
                trello.Cards.Delete(card);
            }
        }

        public static Organization recuperarTime(this Trello trello, string sistema)
        {
            return trello.Organizations.WithId(sistema.ToLower());
        }

        public static Board recuperarQuadroIncidentes(this Trello trello, string sistema)
        {
            var time = trello.recuperarTime(sistema.ToLower());
                
            return trello.Boards.ForOrganization(time)
                .Where(board => board.Name.Equals("incidentes", StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();
        }

        public static List recuperarLista(this Trello trello, string sistema, string lista)
        {
            var quadro = trello.recuperarQuadroIncidentes(sistema.ToLower());

            var todasAsListas = trello.Lists.ForBoard(new BoardId(quadro.GetBoardId()));

            return trello.Lists.ForBoard(new BoardId(quadro.GetBoardId()))
                .Where(l => l.Name.Equals(lista.ToString()))
                .FirstOrDefault();
        }
    }
}
