using System.Linq;

namespace TrelloWrapper
{
    public class Quadro
    {
        private ITrelloConnection trello;

        public Quadro(ITrelloConnection trelloConnection)
        {
            trello = trelloConnection;

            Nome = "Incidentes";
            Submitted = new Lista("Submitted");
            EmInvestigacao = new Lista("Em_Investigacao");
            EmResolucao = new Lista("Em_Resolucao");
            Pendencia = new Lista("Pendencia");
        }

        public string Nome { get; private set; }
        public Lista Submitted { get; private set; }
        public Lista EmInvestigacao { get; private set; }
        public Lista EmResolucao { get; private set; }
        public Lista Pendencia { get; private set; }

        public void MoveCartaoParaEmInvestigacao(Cartao cartao)
        {
            MoveCartaoPara(this, cartao, EmInvestigacao);
        }

        public void MoveCartaoParaEmResolucao(Cartao cartao)
        {
            MoveCartaoPara(this, cartao, EmResolucao);
        }

        public void MoveCartaoParaPendencia(Cartao cartao)
        {
            MoveCartaoPara(this, cartao, Pendencia);
        }

        public void AdicionaCartaoA(Cartao cartao, Lista lista)
        {
            if (CartaoNaoCadastrado(cartao))
            {
                trello.CadastraCartao(cartao);
            }

            lista.Cartoes.Add(cartao);
        }

        private bool CartaoNaoCadastrado(Cartao cartao)
        {
            return Submitted.Cartoes
                        .Union(EmInvestigacao.Cartoes)
                        .Union(EmResolucao.Cartoes)
                        .Union(Pendencia.Cartoes)
                        .Contains(cartao);
        }

        public void RemoveCartaoSeContiver(Quadro quadro, Cartao cartao, Lista lista)
        {
            if (lista.Cartoes.Contains(cartao))
                lista.Cartoes.Remove(cartao);
        }

        private void MoveCartaoPara(Quadro quadro, Cartao cartao, Lista listaDestino)
        {
            RemoveCartaoSeContiver(quadro, cartao, Submitted);
            RemoveCartaoSeContiver(quadro, cartao, EmInvestigacao);
            RemoveCartaoSeContiver(quadro, cartao, EmResolucao);
            RemoveCartaoSeContiver(quadro, cartao, Pendencia);

            AdicionaCartaoA(cartao, listaDestino);

            if (listaDestino == EmInvestigacao)
                trello.MoveParaEmInvestigacao(quadro, cartao);
            else if (listaDestino == EmResolucao)
                trello.MoveParaEmResolucao(quadro, cartao);
            else
                trello.MoveParaPendencia(quadro, cartao);
        }
    }
}