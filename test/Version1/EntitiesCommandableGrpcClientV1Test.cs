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
    public class EntitiesCommandableGrpcClientV1Test: IDisposable
    {
        private EntitiesMemoryPersistence persistence;
        private EntitiesController controller;
        private EntitiesCommandableGrpcServiceV1 service;
        private EntitiesCommandableGrpcClientV1 client;
        private EntitiesClientV1Fixture fixture;

        public EntitiesCommandableGrpcClientV1Test()
        {
            var grpcConfig = ConfigParams.FromTuples(
                "connection.protocol", "http",
                "connection.host", "localhost",
                "connection.port", 3000
            );

            persistence = new EntitiesMemoryPersistence();
            persistence.Configure(new ConfigParams());

            controller = new EntitiesController();
            controller.Configure(new ConfigParams());

            service = new EntitiesCommandableGrpcServiceV1();
            service.Configure(grpcConfig);

            client = new EntitiesCommandableGrpcClientV1();
            client.Configure(grpcConfig);

            var references = References.FromTuples(
                new Descriptor("pip-service-data", "persistence", "memory", "default", "1.0"), persistence,
                new Descriptor("pip-service-data", "controller", "default", "default", "1.0"), controller,
                new Descriptor("pip-service-data", "service", "grpc", "default", "1.0"), service,
                new Descriptor("pip-service-data", "client", "grpc", "default", "1.0"), client
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
