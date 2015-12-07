using NUnit.Framework;
using System.Diagnostics;
using TrelloNet;

namespace TrelloWrapper.Test
{
    [TestFixture]
    public class ConexaoTrello
    {
        [Test]
        public void ConectarTrelloNet()
        {
            var trello = new Trello("950a4f35f078102cc55be50178a55181");
            trello.Authorize("6b7d13c413e8d89cd85e27f1e094f10d879f44c5f18b27f8c594d4cd9b051e9c");

            foreach (var board in trello.Boards.Search("incidentes"))
            {
                Debug.WriteLine(board.Name);
            }
        }
    }
}
