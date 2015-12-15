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
            lista = new Lista("Submitted");
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
    }
}
