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
            var cartaoLocal = mapearIncidenteParaCartaoLocal(incidente);
            enviarCartaoLocalParaTrello(cartaoLocal);

            return cartaoLocal;
        }

        private void enviarCartaoLocalParaTrello(Cartao cartaoLocal)
        {
            trello = new Trello(chave);
            trello.Authorize(token);

            var equipeSistema = trello.Organizations.WithId(cartaoLocal.Sistema.ToLower());

            var incidentes = trello.Boards.ForOrganization(equipeSistema)
                .Where(board => board.Name.Equals("incidentes", StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            var listaSubmitted = trello.Lists.ForBoard(new BoardId(incidentes.GetBoardId()))
                .Where(lista => lista.Name.Equals("Submitted", StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            var novoCartao = new NewCard(cartaoLocal.Nome, new ListId(listaSubmitted.Id));
            var cartaoTrello = trello.Cards.Add(novoCartao);

            adicionarEtiquetaDeSeveridade(cartaoLocal, cartaoTrello);
            adicionarEtiquetaDePrazo(cartaoTrello);
            definirPrazoDeFinalizacao(cartaoLocal, cartaoTrello);
        }

        private void definirPrazoDeFinalizacao(Cartao cartaoLocal, Card cartaoTrello)
        {
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
            else if (cartaoLocal.Severidade == NivelSeveridade.Baixa)
            {
                trello.Cards.AddLabel(cartaoTrello, Color.Blue);
            }
        }

        private Cartao mapearIncidenteParaCartaoLocal(Incidente incidente)
        {
            return new Cartao
            {
                Sistema = incidente.Sistema,
                Nome = string.Format("{0} - {1}", incidente.Id, incidente.Severidade),
                Severidade = incidente.Severidade,
                EstadoSLA = SLA.Novo,
                Lista = ListaEstado.Submitted,
                PrazoFinalizacao = calcularPrazoFinalizacao(incidente)
            };
        }

        private DateTime calcularPrazoFinalizacao(Incidente incidente)
        {
            if(incidente.Severidade == NivelSeveridade.Alta)
            {
                return incidente.DataSubmissao.AddHours(2);
            }

            if (incidente.Severidade == NivelSeveridade.Baixa)
            {
                return incidente.DataSubmissao.AddHours(48);
            }

            return incidente.DataSubmissao;
        }
    }
}