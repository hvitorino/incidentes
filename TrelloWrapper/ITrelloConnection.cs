namespace TrelloWrapper
{
    public interface ITrelloConnection
    {
        void MoveParaEmInvestigacao(Quadro quadro, Cartao cartao);
        void MoveParaEmResolucao(Quadro quadro, Cartao cartao);
        void MoveParaPendencia(Quadro quadro, Cartao cartao);
        void CadastraCartao(Cartao cartao, Lista lista);
    }
}