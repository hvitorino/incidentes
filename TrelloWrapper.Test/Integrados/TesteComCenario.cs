using NUnit.Framework;
using TrelloNet;

namespace TrelloWrapper.Test.Integrados
{
    [TestFixture]
    public abstract class TesteComCenario
    {
        protected Quadro quadro;
        protected Trello trello;

        public TesteComCenario()
        {
            trello = TrelloFactory.API;
        }

        [OneTimeTearDown]
        public void DesfazCenario()
        {
            TrelloHelper.ExcluiCartoes(quadro);
        }
    }
}
