using PipTemplatesClientData.Clients.Version1;

using System.Threading.Tasks;
using Xunit;

namespace PipTemplates.Client.Data.Test.Version1
{
    public class EntitiesMockClientV1Test
    {
        private EntitiesMockClientV1 client;
        private EntitiesClientV1Fixture fixture;

        public EntitiesMockClientV1Test()
        {
            client = new EntitiesMockClientV1();
            fixture = new EntitiesClientV1Fixture(client);
        }

        [Fact]
        public async Task TestCrudOperations()
        {
            await fixture.TestCrudOperations();
        }
    }
}
