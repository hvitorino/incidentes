using NUnit.Framework;

namespace TrelloWrapper.Test
{
    [TestFixture]
    public abstract class TesteComCenario
    {
        [OneTimeTearDown]
        public void DesfazCenario()
        {
            //TrelloFactory.API.excluirIncidentes("s160");
        }
    }
}
