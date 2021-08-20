using PipServices3.Commons.Config;
using PipServices3.Commons.Refer;
using PipTemplatesClientData.Clients.Version1;
using PipTemplatesServiceData.Logic;
using PipTemplatesServiceData.Persistence;
using PipTemplatesServiceData.Services.Version1;

using System;
using System.Threading.Tasks;
using Xunit;

namespace PipTemplates.Client.Data.Test.Version1
{
    [Collection("Sequential")]
    public class EntitiesCommandableHttpClientV1Test: IDisposable
    {
        private EntitiesMemoryPersistence persistence;
        private EntitiesController controller;
        private EntitiesCommandableHttpServiceV1 service;
        private EntitiesCommandableHttpClientV1 client;
        private EntitiesClientV1Fixture fixture;

        public EntitiesCommandableHttpClientV1Test()
        {
            var httpConfig = ConfigParams.FromTuples(
                "connection.protocol", "http",
                "connection.host", "localhost",
                "connection.port", 3000

            );

            persistence = new EntitiesMemoryPersistence();
            persistence.Configure(new ConfigParams());

            controller = new EntitiesController();
            controller.Configure(new ConfigParams());

            service = new EntitiesCommandableHttpServiceV1();
            service.Configure(httpConfig);

            client = new EntitiesCommandableHttpClientV1();
            client.Configure(httpConfig);

            var references = References.FromTuples(
                new Descriptor("pip-service-data", "persistence", "memory", "default", "1.0"), persistence,
                new Descriptor("pip-service-data", "controller", "default", "default", "1.0"), controller,
                new Descriptor("pip-service-data", "service", "http", "default", "1.0"), service,
                new Descriptor("pip-service-data", "client", "http", "default", "1.0"), client
            );
            controller.SetReferences(references);
            service.SetReferences(references);
            client.SetReferences(references);

            fixture = new EntitiesClientV1Fixture(client);


            persistence.OpenAsync(null).Wait();
            service.OpenAsync(null).Wait();
            client.OpenAsync(null).Wait();
        }

        public void Dispose()
        {
            client.CloseAsync(null).Wait();
            service.CloseAsync(null).Wait();
            persistence.ClearAsync(null).Wait();
        }

        [Fact]
        public async Task TestCrudOperations()
        {
            await fixture.TestCrudOperations();
        }
    }
}
