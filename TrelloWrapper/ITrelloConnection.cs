namespace TrelloWrapper
{
    public interface ITrelloConnection
    {
        void CadastraCartao(Cartao cartao);
        void MoveParaEmInvestigacao(Quadro quadro, Cartao cartao);
        void MoveParaEmResolucao(Quadro quadro, Cartao cartao);
        void MoveParaPendencia(Quadro quadro, Cartao cartao);
    }
}