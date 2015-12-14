using NUnit.Framework;
using System;

namespace TrelloWrapper.Test
{
    [TestFixture]
    public class UmCartao
    {
        private Cartao cartao;

        [OneTimeSetUp]
        public void Cenario()
        {
            cartao = new Cartao
            {
                Nome = "Cartão 1",
                DataSubmissao = DateTime.Now,
                PrazoFinalizacao = DateTime.Now.AddHours(2),
                Severidade = NivelSeveridade.Alta
            };
        }

        [Test]
        public void TemNome()
        {
            Assert.That(cartao.Nome, Is.Not.Null);
        }

        [Test]
        public void TemDataDeSubmissao()
        {
            Assert.That(cartao.DataSubmissao, Is.Not.Null);
        }

        [Test]
        public void TemPrazoDeFinalizacao()
        {
            Assert.That(cartao.PrazoFinalizacao, Is.Not.Null);
        }

        [Test]
        public void TemSeveridade()
        {
            Assert.That(cartao.Severidade, Is.Not.Null);
        }
    }
}
