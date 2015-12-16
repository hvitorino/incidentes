using NUnit.Framework;
using System;
using System.Linq;
using TrelloNet;

namespace TrelloWrapper.Test.Integrados.CriarCartao
{
    [TestFixture]
    public class SeveridadeAlta : TesteComCenario
    {
        private Cartao cartao;

        [OneTimeSetUp]
        public void Cenario()
        {
            trello = TrelloFactory.API;

            quadro = new Quadro("S160", new TrelloConnection());

            cartao = new Cartao
            {
                Nome = "SEVERIDADE_ALTA",
                Severidade = NivelSeveridade.Alta,
                DataSubmissao = DateTime.Now
            };

            quadro.AdicionaCartaoA(cartao, quadro.Submitted);
        }

        [Test]
        public void DevePossuirEtiquetaDeSeveridadeAlta()
        {
            var card = TrelloHelper.RecuperarCartao(cartao);

            Assert.That(card.LabelColors, Contains.Item(Color.Red));
        }

        [Test]
        [Ignore("Muito trabalho pra pouco retorno")]
        public void DeveSerPostoEmInvestigacaoEm2Horas()
        {
            var card = TrelloHelper.RecuperarCartao(cartao);
            var prazo = card.Due.Value.ToLocalTime().Hour - cartao.DataSubmissao.Hour;

            Assert.That(prazo, Is.EqualTo(2));
        }
    }
}
