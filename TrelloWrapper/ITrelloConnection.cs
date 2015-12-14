namespace TrelloWrapper
{
    public interface ITrelloConnection
    {
        Cartao cadastrarIncidente(Incidente incidente);
        void moverParaEmInvestigacao(Cartao cartao);
        void moverParaEmResolucao(Cartao cartao);
        void moverParaPendencia(Cartao cartao);
    }
}