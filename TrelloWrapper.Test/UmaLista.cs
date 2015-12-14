using NUnit.Framework;
using System.Collections.Generic;

namespace TrelloWrapper.Test
{
    [TestFixture]
    public class UmaLista
    {
        private Lista lista;

        [OneTimeSetUp]
        public void Cenario()
        {
            lista = new Lista
            {
                Nome = "Submitted",
                Cartoes = new List<Cartao>()
            };
        }

        [Test]
        public void TemNome()
        {
            Assert.That(lista.Nome, Is.EqualTo("Submitted"));
        }

        [Test]
        public void TemCartoes()
        {
            Assert.That(lista.Cartoes, Is.Not.Null);
        }

        [Test]
        public void PodeReceberNovoCartao()
        {
            var novoCartao = new Cartao();

            lista.AdicionaCartao(novoCartao);

            Assert.That(lista.Cartoes, Contains.Item(novoCartao));
        }
    }
}
