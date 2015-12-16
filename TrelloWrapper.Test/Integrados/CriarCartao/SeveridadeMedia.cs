using NUnit.Framework;
using System;
using TrelloNet;

namespace TrelloWrapper.Test.Integrados.CriarCartao
{
    [TestFixture]
    public class SeveridadeMedia : TesteComCenario
    {
        private Cartao cartao;

        [OneTimeSetUp]
        public void Cenario()
        {
            trello = TrelloFactory.API;

            quadro = new Quadro("S160", new TrelloConnection());

            cartao = new Cartao
            {
                Nome = "SEVERIDADE_MEDIA",
                Severidade = NivelSeveridade.Media,
                DataSubmissao = DateTime.Now
            };

            quadro.AdicionaCartaoA(cartao, quadro.Submitted);
        }

        [Test]
        public void DevePossuirSeveridadeMedia()
        {
            var card = TrelloHelper.RecuperarCartao(cartao);

            Assert.That(card.LabelColors, Contains.Item(Color.Orange));
        }

        [Test]
        [Ignore("Muito trabalho pra pouco retorno")]
        public void DeveSerPostoEmInvestigacaoEm5Horas()
        {
            var card = TrelloHelper.RecuperarCartao(cartao);
            var prazo = card.Due.Value.Subtract(cartao.DataSubmissao).Hours;

            Assert.That(prazo, Is.EqualTo(5));
        }
    }
}
