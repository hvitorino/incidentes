using TrelloNet;

namespace TrelloWrapper
{
    public class TrelloFactory
    {
        private const string chave = "950a4f35f078102cc55be50178a55181";
        private const string token = "6b7d13c413e8d89cd85e27f1e094f10d879f44c5f18b27f8c594d4cd9b051e9c";

        private static Trello trello;

        static TrelloFactory()
        {
            trello = new Trello(chave);
            trello.Authorize(token);
        }

        public static Trello API
        {
            get { return trello; }
        }
    }
}
