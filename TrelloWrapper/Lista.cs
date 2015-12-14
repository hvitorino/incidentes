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
    }
}