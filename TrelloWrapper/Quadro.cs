namespace TrelloWrapper
{
    public class Quadro
    {
        private ITrelloConnection trello;

        public Quadro(ITrelloConnection trelloConnection)
        {
            trello = trelloConnection;

            Nome = "Incidentes";
            Submitted = new Lista();
            EmInvestigacao = new Lista();
            EmResolucao = new Lista();
            Pendencia = new Lista();
        }

        public string Nome { get; private set; }
        public Lista Submitted { get; private set; }
        public Lista EmInvestigacao { get; private set; }
        public Lista EmResolucao { get; private set; }
        public Lista Pendencia { get; private set; }

        public void MoveCartaoParaEmInvestigacao(Cartao cartao)
        {
            MoveCartaoPara(cartao, EmInvestigacao);
        }

        public void MoveCartaoParaEmResolucao(Cartao cartao)
        {
            MoveCartaoPara(cartao, EmResolucao);
        }

        public void MoveCartaoParaPendencia(Cartao cartao)
        {
            MoveCartaoPara(cartao, Pendencia);
        }

        public void AdicionaCartaoA(Cartao cartao, Lista lista)
        {
            lista.Cartoes.Add(cartao);
        }

        public void RemoveCartaoSeContiver(Cartao cartao, Lista lista)
        {
            if (lista.Cartoes.Contains(cartao))
                lista.Cartoes.Remove(cartao);
        }

        private void MoveCartaoPara(Cartao cartao, Lista listaDestino)
        {
            RemoveCartaoSeContiver(cartao, Submitted);
            RemoveCartaoSeContiver(cartao, EmInvestigacao);
            RemoveCartaoSeContiver(cartao, EmResolucao);
            RemoveCartaoSeContiver(cartao, Pendencia);

            AdicionaCartaoA(cartao, listaDestino);
        }
    }
}