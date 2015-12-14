using NUnit.Framework;
using System.Collections.Generic;

namespace TrelloWrapper.Test
{
    [TestFixture]
    public class UmQuadro
    {
        private Quadro quadro;

        [OneTimeSetUp]
        public void Cenario()
        {
            quadro = new Quadro
            {
                Nome = "S160",
                Listas = new List<Lista>()
            };
        }

        [Test]
        public void TemNome()
        {
            Assert.That(quadro.Nome, Is.EqualTo("S160"));
        }

        [Test]
        public void TemListas()
        {
            Assert.That(quadro.Listas, Is.Not.Null);
        }
    }
}
