using System;

namespace TrelloWrapper
{
    public class Cartao
    {
        public SLA EstadoSLA { get; set; }
        public NivelSeveridade Severidade { get; set; }
        public ListaEstado Lista { get; set; }
        public string Nome { get; set; }
        public DateTime PrazoFinalizacao { get; set; }
        public string Sistema { get; set; }
        public int ShortIdTrello { get; set; }
        public DateTime DataSubmissao { get; set; }
    }
}