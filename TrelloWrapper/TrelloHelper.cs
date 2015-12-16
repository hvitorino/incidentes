using TrelloNet;
using System.Linq;
using System;

namespace TrelloWrapper
{
    public static class TrelloHelper
    {
        public static Card RecuperaCartao(Cartao cartao)
        {
            var trello = TrelloFactory.API;

            var org = trello.Organizations.Search(cartao.Lista.Quadro.Equipe).SingleOrDefault();

            var board = trello.Boards.ForOrganization(new OrganizationId(org.Id))
                .Where(b => b.Name.Equals(cartao.Lista.Quadro.Nome, StringComparison.OrdinalIgnoreCase))
                .SingleOrDefault();

            return trello.Cards.WithShortId(cartao.IdShortTrello, new BoardId(board.Id));
        }

        public static List RecuperaLista(Lista lista)
        {
            var trello = TrelloFactory.API;

            var org = trello.Organizations.Search(lista.Quadro.Equipe).SingleOrDefault();

            var board = trello.Boards.ForOrganization(new OrganizationId(org.Id))
                .Where(b => b.Name.Equals(lista.Quadro.Nome, StringComparison.OrdinalIgnoreCase))
                .SingleOrDefault();

            return trello.Lists.ForBoard(new BoardId(board.Id))
                .Where(l => l.Name.Equals(lista.Nome, StringComparison.OrdinalIgnoreCase))
                .SingleOrDefault();
        }

        public static void ExcluiCartoes(Quadro quadro)
        {
            var trello = TrelloFactory.API;

            var time = trello.Organizations.WithId(quadro.Equipe.ToLower());

            var trelloBoard = trello.Boards.ForOrganization(time)
                .Where(board => board.Name.Equals(quadro.Nome, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            var allCards = trello.Cards.ForBoard(trelloBoard);

            foreach (var card in allCards)
            {
                trello.Cards.Delete(card);
            }
        }
    }
}
