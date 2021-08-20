
using PipServices3.Commons.Config;
using PipServices3.Commons.Refer;
using PipServices3.Components.Log;
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
    public class EntitiesRestClientV1Test : IDisposable
    {
        private EntitiesRestServiceV1 service;
        private EntitiesRestClientV1 client;
        private EntitiesClientV1Fixture fixture;


        public EntitiesRestClientV1Test()
        {
            var httpConfig = ConfigParams.FromTuples(
                "connection.protocol", "http",
                "connection.host", "localhost",
                "connection.port", 3000
            );

            var logger = new ConsoleLogger();
            var persistence = new EntitiesMemoryPersistence();
            var controller = new EntitiesController();

            service = new EntitiesRestServiceV1();
            service.Configure(httpConfig);

            var references = References.FromTuples(
                new Descriptor("pip-services-commons", "logger", "console", "default", "1.0"), logger,
                new Descriptor("pip-service-data", "persistence", "memory", "default", "1.0"), persistence,
                new Descriptor("pip-service-data", "controller", "default", "default", "1.0"), controller,
                new Descriptor("pip-service-data", "service", "rest", "default", "1.0"), service
            );

            
            controller.SetReferences(references);
            service.SetReferences(references);

            client = new EntitiesRestClientV1();
            client.SetReferences(references);
            client.Configure(httpConfig);

            fixture = new EntitiesClientV1Fixture(client);

            service.OpenAsync(null).Wait();
            client.OpenAsync(null).Wait();

        }

        public void Dispose()
        {
            client.CloseAsync(null).Wait();
            service.CloseAsync(null).Wait();
        }

        [Fact]
        public async Task TestCrudOperations()
        {
            await fixture.TestCrudOperations();
        }
    }
}
