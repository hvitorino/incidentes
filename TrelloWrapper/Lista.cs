using System.Collections.Generic;

namespace TrelloWrapper
{
    public class Lista
    {
        public Lista()
        {
            Cartoes = new List<Cartao>();
        }

        public List<Cartao> Cartoes { get; set; }
        public string Nome { get; set; }

        public void AdicionaCartao(Cartao cartao)
        {
            Cartoes.Add(cartao);
        }

        public void RemoveCartaoSeContiver(Cartao cartao)
        {
            if (Cartoes.Contains(cartao))
                Cartoes.Remove(cartao);
        }
    }
}