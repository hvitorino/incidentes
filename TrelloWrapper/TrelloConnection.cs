using System;
using System.Linq;
using TrelloNet;

namespace TrelloWrapper
{
    public class TrelloConnection : IDisposable, ITrelloConnection
    {
        private const string chave = "950a4f35f078102cc55be50178a55181";
        private const string token = "6b7d13c413e8d89cd85e27f1e094f10d879f44c5f18b27f8c594d4cd9b051e9c";

        private Trello trello;

        public TrelloConnection()
        {
            trello = new Trello(chave);
            trello.Authorize(token);
        }

        public void CadastraCartao(Cartao cartao)
        {
            List listaSubmitted = RecuperaListaSubmitted(cartao);

            var novoCartao = new NewCard(cartao.Nome, new ListId(listaSubmitted.Id));
            var cartaoTrello = trello.Cards.Add(novoCartao);

            cartao.ShortIdTrello = cartaoTrello.IdShort;

            adicionarEtiquetaDeSeveridade(cartao, cartaoTrello);
            adicionarEtiquetaDePrazo(cartaoTrello);
            definirPrazoDeFinalizacao(cartao, cartaoTrello);
        }

        private List RecuperaListaSubmitted(Cartao cartao)
        {
            var equipeSistema = trello.Organizations.WithId(cartao.Sistema.ToLower());

            var incidentes = trello.Boards.ForOrganization(equipeSistema)
                .Where(board => board.Name.Equals("incidentes", StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            var listaSubmitted = trello.Lists.ForBoard(new BoardId(incidentes.GetBoardId()))
                .Where(lista => lista.Name.Equals("Submitted", StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();
            return listaSubmitted;
        }

        public void MoveParaEmInvestigacao(Cartao cartao)
        {
            MoveCartao(cartao, ListaEstado.Em_Investigacao);
        }

        public void MoveParaPendencia(Cartao cartao)
        {
            MoveCartao(cartao, ListaEstado.Pendencia);
        }

        public void MoveParaEmResolucao(Cartao cartao)
        {
            MoveCartao(cartao, ListaEstado.Em_Resolucao);
        }

        private void MoveCartao(Cartao cartao, ListaEstado lista)
        {
            var quadro = trello.recuperarQuadroIncidentes(cartao.Sistema);
            var listaDestino = trello.recuperarLista(cartao.Sistema, lista);
            var cartaoTrello = trello.Cards.WithShortId(cartao.ShortIdTrello, quadro);

            trello.Cards.Move(cartaoTrello, listaDestino);

            cartao.Lista = lista;
        }

        private void definirPrazoDeFinalizacao(Cartao cartaoLocal, Card cartaoTrello)
        {
            cartaoLocal.PrazoFinalizacao = calcularPrazoFinalizacao(cartaoLocal);

            trello.Cards.ChangeDueDate(cartaoTrello, cartaoLocal.PrazoFinalizacao);
        }

        private void adicionarEtiquetaDePrazo(Card cartaoTrello)
        {
            trello.Cards.AddLabel(cartaoTrello, Color.Green);
        }

        private void adicionarEtiquetaDeSeveridade(Cartao cartaoLocal, Card cartaoTrello)
        {
            if (cartaoLocal.Severidade == NivelSeveridade.Alta)
            {
                trello.Cards.AddLabel(cartaoTrello, Color.Red);
            }
            else if (cartaoLocal.Severidade == NivelSeveridade.Media)
            {
                trello.Cards.AddLabel(cartaoTrello, Color.Orange);
            }
            else if (cartaoLocal.Severidade == NivelSeveridade.Baixa)
            {
                trello.Cards.AddLabel(cartaoTrello, Color.Blue);
            }
        }

        private DateTime calcularPrazoFinalizacao(Cartao cartao)
        {
            if(cartao.Severidade == NivelSeveridade.Alta)
            {
                return cartao.DataSubmissao.AddHours(2);
            }

            if (cartao.Severidade == NivelSeveridade.Baixa)
            {
                return cartao.DataSubmissao.AddHours(48);
            }

            return cartao.DataSubmissao;
        }

        public void Dispose()
        {
            trello.Deauthorize();
        }
    }
}