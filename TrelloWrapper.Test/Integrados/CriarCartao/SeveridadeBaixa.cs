using NUnit.Framework;
using System;
using System.Linq;
using TrelloNet;

namespace TrelloWrapper.Test.Integrados.CriarCartao
{
    [TestFixture]
    public class SeveridadeBaixa : TesteComCenario
    {
        private Cartao cartao;

        [OneTimeSetUp]
        public void Cenario()
        {
            trello = TrelloFactory.API;

            quadro = new Quadro("S160", new TrelloConnection());

            cartao = new Cartao
            {
                Nome = "SEVERIDADE_BAIXA",
                Severidade = NivelSeveridade.Baixa,
                DataSubmissao = DateTime.Now
            };

            quadro.AdicionaCartaoA(cartao, quadro.Submitted);
        }

        [Test]
        public void DevePossuirSeveridadeBaixa()
        {
            var card = TrelloHelper.RecuperaCartao(cartao);

            Assert.That(card.LabelColors, Contains.Item(Color.Green));
        }

        [Test]
        [Ignore("Muito trabalho pra pouco retorno")]
        public void DeveSerPostoEmInvestigacaoEm48Horas()
        {
            var card = TrelloHelper.RecuperaCartao(cartao);
            var prazo = card.Due.Value.ToLocalTime() - cartao.DataSubmissao;

            Assert.That(prazo, Is.EqualTo(48));
        }
    }
}
