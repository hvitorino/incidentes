using System;

namespace TrelloWrapper
{
    public class Incidente
    {
        public string Id { get; set; }
        public NivelSeveridade Severidade { get; set; }
        public string Sistema { get; set; }
        public DateTime DataSubmissao { get; set; }
    }
}