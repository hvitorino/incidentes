using NUnit.Framework;
using System.Diagnostics;
using TrelloNet;

namespace TrelloWrapper.Test
{
    [TestFixture]
    public class ConexaoTrello
    {
        [Test]
        public void ConetarTrelloNet()
        {
            var trello = new Trello("950a4f35f078102cc55be50178a55181");
            trello.Authorize("7fd6a41ed8d4b253b9f49465f025898b980b103ddea5a4bcbee243230d9e9783");

            foreach (var team in trello.Organizations.Search("S160"))
            {
                Debug.WriteLine(team.DisplayName);
            }
        }
    }
}
