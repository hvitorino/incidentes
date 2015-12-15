using System.Collections.Generic;

namespace TrelloWrapper
{
    public class Lista
    {
        public Lista(string nome)
        {
            Cartoes = new List<Cartao>();
            Nome = nome;
        }

        public List<Cartao> Cartoes { get; private set; }
        public string Nome { get; private set; }
    }
}