using System.Collections.Generic;

namespace TrelloWrapper
{
    public class Lista
    {
        public Lista(Quadro quadro, string nome)
        {
            Quadro = quadro;
            Cartoes = new List<Cartao>();
            Nome = nome;
        }

        public Quadro Quadro { get; private set; }
        public List<Cartao> Cartoes { get; private set; }
        public string Nome { get; private set; }
    }
}