using System.Linq;

namespace TrelloWrapper
{
    public class Quadro
    {
        public Quadro()
        {
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

        public void MoveCartaoPara(Cartao cartao, Lista listaDestino)
        {
            Submitted.RemoveCartaoSeContiver(cartao);
            EmInvestigacao.RemoveCartaoSeContiver(cartao);
            EmResolucao.RemoveCartaoSeContiver(cartao);
            Pendencia.RemoveCartaoSeContiver(cartao);

            listaDestino.AdicionaCartao(cartao);
        }
    }
}