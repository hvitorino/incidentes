using NUnit.Framework;

namespace TrelloWrapper.Test.Unitarios
{
    [TestFixture]
    public class UmaLista
    {
        private Lista lista;

        [OneTimeSetUp]
        public void Cenario()
        {
            lista = new Lista(new Quadro("S160", new TrelloConnection()), "Submitted");
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
