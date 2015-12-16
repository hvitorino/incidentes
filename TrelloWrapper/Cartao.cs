using System;

namespace TrelloWrapper
{
    public class Cartao
    {
        public NivelSeveridade Severidade { get; set; }
        public string Nome { get; set; }
        public DateTime PrazoFinalizacao { get; set; }
        public int IdShortTrello { get; set; }
        public DateTime DataSubmissao { get; set; }
        public Lista Lista { get; internal set; }
    }
}