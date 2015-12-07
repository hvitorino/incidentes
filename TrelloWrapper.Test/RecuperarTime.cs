using NUnit.Framework;
using System.Linq;
using TrelloNet;

namespace TrelloWrapper.Test
{
    [TestFixture]
    public class RecuperarTime
    {
        private const string chave = "950a4f35f078102cc55be50178a55181";
        private const string token = "6b7d13c413e8d89cd85e27f1e094f10d879f44c5f18b27f8c594d4cd9b051e9c";

        private Trello trello;

        public RecuperarTime()
        {
            trello = new Trello(chave);
            trello.Authorize(token);
        }

        [Test]
        public void RecuperarS160()
        {
            var org = trello.Organizations.Search("s160").SingleOrDefault();

            Assert.That(org, Is.Not.Null);
            Assert.That(org.Name.ToLower(), Is.EqualTo("s160"));
        }
    }
}
