using System;

namespace TrelloWrapper
{
    public class Incidente
    {
        public DateTime DataSubmissao { get; internal set; }
        public string Id { get; set; }
        public object Severidade { get; set; }
        public string Sistema { get; set; }
    }
}