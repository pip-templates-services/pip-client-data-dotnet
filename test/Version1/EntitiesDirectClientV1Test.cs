using PipServices3.Commons.Config;
using PipServices3.Commons.Refer;
using PipTemplatesClientData.Clients.Version1;
using PipTemplatesServiceData.Logic;
using PipTemplatesServiceData.Persistence;

using System;
using System.Threading.Tasks;
using Xunit;

namespace PipTemplates.Client.Data.Test.Version1
{
    public class EntitiesDirectClientV1Test: IDisposable
    {
        private EntitiesMemoryPersistence persistence;
        private EntitiesController controller;
        private EntitiesDirectClientV1 client;
        private EntitiesClientV1Fixture fixture;

        public EntitiesDirectClientV1Test()
        {
            persistence = new EntitiesMemoryPersistence();
            controller = new EntitiesController();
            client = new EntitiesDirectClientV1();

            persistence.Configure(new ConfigParams());
            controller.Configure(new ConfigParams());

            

            var references = References.FromTuples(
                new Descriptor("pip-service-data", "persistence", "memory", "default", "1.0"), persistence,
                new Descriptor("pip-service-data", "controller", "default", "default", "1.0"), controller,
                new Descriptor("pip-service-data", "client", "direct", "default", "1.0"), client
                
            );

            controller.SetReferences(references);
            client.SetReferences(references);

            fixture = new EntitiesClientV1Fixture(client);

            persistence.OpenAsync(null).Wait();
            client.OpenAsync(null).Wait();
        }

        public void Dispose()
        {
            persistence.ClearAsync(null).Wait();
            client.CloseAsync(null).Wait();
        }

        [Fact]
        public async Task TestCrudOperations()
        {
            await fixture.TestCrudOperations();
        }
    }
}
