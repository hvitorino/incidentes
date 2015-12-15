namespace TrelloWrapper
{
    public interface ITrelloConnection
    {
        void CadastraCartao(Cartao cartao);
        void MoveParaEmInvestigacao(Cartao cartao);
        void MoveParaEmResolucao(Cartao cartao);
        void MoveParaPendencia(Cartao cartao);
    }
}