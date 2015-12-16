using System;
using System.Linq;

namespace TrelloWrapper
{
    public class Quadro
    {
        private readonly ITrelloConnection trello;
        private readonly CalculadoraPrazo calculadoraPrazo = new CalculadoraPrazo();

        public Quadro(string equipe, ITrelloConnection trelloConnection)
        {
            trello = trelloConnection;
            Equipe = equipe;

            Nome = "Incidentes";
            Submitted = new Lista(this, "Submitted");
            EmInvestigacao = new Lista(this, "Em_Investigacao");
            EmResolucao = new Lista(this, "Em_Resolucao");
            Pendencia = new Lista(this, "Pendencia");
        }

        public string Equipe { get; private set; }
        public string Nome { get; private set; }
        public Lista Submitted { get; private set; }
        public Lista EmInvestigacao { get; private set; }
        public Lista EmResolucao { get; private set; }
        public Lista Pendencia { get; private set; }

        public void MoveCartaoParaEmInvestigacao(Cartao cartao)
        {
            MoveCartaoPara(this, cartao, EmInvestigacao);
        }

        public void MoveCartaoParaEmResolucao(Cartao cartao)
        {
            MoveCartaoPara(this, cartao, EmResolucao);
        }

        public void MoveCartaoParaPendencia(Cartao cartao)
        {
            MoveCartaoPara(this, cartao, Pendencia);
        }

        public void AdicionaCartaoA(Cartao cartao, Lista lista)
        {
            if (CartaoNaoCadastrado(cartao))
            {
                cartao.PrazoFinalizacao = calculadoraPrazo.Calcula(cartao);
                trello.CadastraCartao(cartao, lista);
            }

            cartao.Lista = lista;
            lista.Cartoes.Add(cartao);
        }

        public void RemoveCartaoSeContiver(Quadro quadro, Cartao cartao, Lista lista)
        {
            if (lista.Cartoes.Contains(cartao))
                lista.Cartoes.Remove(cartao);
        }

        private void MoveCartaoPara(Quadro quadro, Cartao cartao, Lista listaDestino)
        {
            RemoveCartaoSeContiver(quadro, cartao, Submitted);
            RemoveCartaoSeContiver(quadro, cartao, EmInvestigacao);
            RemoveCartaoSeContiver(quadro, cartao, EmResolucao);
            RemoveCartaoSeContiver(quadro, cartao, Pendencia);

            listaDestino.Cartoes.Add(cartao);

            if (listaDestino == EmInvestigacao)
                trello.MoveParaEmInvestigacao(quadro, cartao);
            else if (listaDestino == EmResolucao)
                trello.MoveParaEmResolucao(quadro, cartao);
            else
                trello.MoveParaPendencia(quadro, cartao);
        }

        private bool CartaoNaoCadastrado(Cartao cartao)
        {
            return !Submitted.Cartoes
                        .Union(EmInvestigacao.Cartoes)
                        .Union(EmResolucao.Cartoes)
                        .Union(Pendencia.Cartoes)
                        .Contains(cartao);
        }

        private class CalculadoraPrazo
        {
            public DateTime Calcula(Cartao cartao)
            {
                DateTime prazo;

                if (cartao.Severidade == NivelSeveridade.Alta)
                {
                    prazo = new DateTime(cartao.DataSubmissao.Ticks).AddHours(2);
                }
                else if (cartao.Severidade == NivelSeveridade.Media)
                {
                    prazo = new DateTime(cartao.DataSubmissao.Ticks).AddHours(5);
                }
                else //if (cartao.Severidade == NivelSeveridade.Baixa)
                {
                    prazo = new DateTime(cartao.DataSubmissao.Ticks).AddHours(48);
                }

                return prazo;
            }
        }
    }
}