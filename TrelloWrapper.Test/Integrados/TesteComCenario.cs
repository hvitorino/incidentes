using NUnit.Framework;

namespace TrelloWrapper.Test.Integrados
{
    [TestFixture]
    public abstract class TesteComCenario
    {
        [OneTimeTearDown]
        public void DesfazCenario()
        {
            TrelloFactory.API.excluirIncidentes("s160");
        }
    }
}
