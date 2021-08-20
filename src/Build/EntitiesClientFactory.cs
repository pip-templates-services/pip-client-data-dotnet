using PipTemplatesClientData.Clients.Version1;
using PipServices3.Commons.Refer;
using PipServices3.Components.Build;

namespace PipTemplatesClientData.Build
{
    public class EntitiesClientFactory : Factory
    {
        public static Descriptor NullClientDescriptor = new Descriptor("pip-service-data", "client", "null", "*", "1.0");
        public static Descriptor DirectClientDescriptor = new Descriptor("pip-service-data", "client", "direct", "*", "1.0");
        public static Descriptor CommandableHttpClientDescriptor = new Descriptor("pip-service-data", "client", "commandable-http", "*", "1.0");
        public static Descriptor CommandableGrpcClientV1Descriptor = new Descriptor("pip-service-data", "client", "commandable-grpc", "*", "1.0");
        public static Descriptor CommandableLambdaClientV1Descriptor = new Descriptor("pip-service-data", "client", "commandable-lambda", "*", "1.0");
        public static Descriptor LambdaClientV1Descriptor = new Descriptor("pip-service-data", "client", "lambda", "default", "1.0");
        public static Descriptor GrpcClientV1Descriptor = new Descriptor("pip-service-data", "client", "grpc", "*", "1.0");
        public static Descriptor RestClientV1Descriptor = new Descriptor("pip-service-data", "client", "rest", "*", "1.0");
        public static Descriptor EntitiesMockClientV1Descriptor = new Descriptor("pip-service-data", "client", "mock", "*", "1.0");

        public EntitiesClientFactory() : base()
        {
            RegisterAsType(EntitiesClientFactory.NullClientDescriptor, typeof(EntitiesNullClientV1));
            RegisterAsType(EntitiesClientFactory.EntitiesMockClientV1Descriptor, typeof(EntitiesMockClientV1));
            RegisterAsType(EntitiesClientFactory.DirectClientDescriptor, typeof(EntitiesDirectClientV1));
            RegisterAsType(EntitiesClientFactory.CommandableHttpClientDescriptor, typeof(EntitiesCommandableHttpClientV1));
            RegisterAsType(EntitiesClientFactory.CommandableGrpcClientV1Descriptor, typeof(EntitiesCommandableGrpcClientV1));
            // RegisterAsType(EntitiesClientFactory.RestClientV1Descriptor, typeof(EntitiesRestClientV1));
            // RegisterAsType(EntitiesClientFactory.GrpcClientV1Descriptor, typeof(EntitiesGrpcClientV1));
            // RegisterAsType(EntitiesClientFactory.LambdaClientV1Descriptor, typeof(EntitiesLambdaClientV1));
        }
    }
}
