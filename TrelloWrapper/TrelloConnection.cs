using System;
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

        public void CadastraCartao(Cartao cartao, Lista lista)
        {
            List trelloList = TrelloHelper.RecuperaLista(lista);

            var novoCartao = new NewCard(cartao.Nome, new ListId(trelloList.Id));
            var cartaoTrello = trello.Cards.Add(novoCartao);

            cartao.IdShortTrello = cartaoTrello.IdShort;

            AdicionaEtiquetaDeSeveridade(cartao, cartaoTrello);
            AdicionaEtiquetaDePrazo(cartaoTrello);
            RegistraPrazoDeFinalizacao(cartao, cartaoTrello);
        }

        public void MoveParaEmInvestigacao(Quadro quadro, Cartao cartao)
        {
            MoveCartao(cartao, quadro.EmInvestigacao);
        }

        public void MoveParaPendencia(Quadro quadro, Cartao cartao)
        {
            MoveCartao(cartao, quadro.Pendencia);
        }

        public void MoveParaEmResolucao(Quadro quadro, Cartao cartao)
        {
            MoveCartao(cartao, quadro.EmResolucao);
        }

        private void MoveCartao(Cartao cartao, Lista listaDestino)
        {
            var cartaoTrello = TrelloHelper.RecuperaCartao(cartao);
            var listaTrello = TrelloHelper.RecuperaLista(listaDestino);

            trello.Cards.Move(cartaoTrello, new ListId(listaTrello.Id));

            cartao.Lista = listaDestino;
        }

        private void RegistraPrazoDeFinalizacao(Cartao cartaoLocal, Card cartaoTrello)
        {
            trello.Cards.ChangeDueDate(cartaoTrello, cartaoLocal.PrazoFinalizacao);
        }

        private void AdicionaEtiquetaDePrazo(Card cartaoTrello)
        {
            trello.Cards.AddLabel(cartaoTrello, Color.Green);
        }

        private void AdicionaEtiquetaDeSeveridade(Cartao cartaoLocal, Card cartaoTrello)
        {
            if (cartaoLocal.Severidade == NivelSeveridade.Alta)
            {
                trello.Cards.AddLabel(cartaoTrello, Color.Red);
            }
            else if (cartaoLocal.Severidade == NivelSeveridade.Media)
            {
                trello.Cards.AddLabel(cartaoTrello, Color.Orange);
            }
            else //if (cartaoLocal.Severidade == NivelSeveridade.Baixa)
            {
                trello.Cards.AddLabel(cartaoTrello, Color.Blue);
            }
        }

        public void Dispose()
        {
            trello.Deauthorize();
        }
    }
}