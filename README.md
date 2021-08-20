# <img src="https://uploads-ssl.webflow.com/5ea5d3315186cf5ec60c3ee4/5edf1c94ce4c859f2b188094_logo.svg" alt="Pip.Services Logo" width="200"> <br/> Client library for sample data microservice

This is a client library to the sample data microservice. This library shall be used
as a template to create clients to general purpose data microservices.

Supported functionality:
* Null and Mock clients for testing
* HTTP clients: REST and Commandable
* GRPC clients: Plain and Commandable

Key patterns implemented in this library:

**Zero-time onboarding:** A new developer doesn't have to have a prior khowledge of the code
nor preinstalled and preconfigured development environment.
To get started with any component he/she just need to do 3 simple steps:
+ Checkout the code
+ Launch dependencies via [docker-compose.dev.yml](docker/docker-compose.dev.yml)
+ Execute **dotnet test**. 

**Automated build and test processes:** Clear, build and test actions are dockerized and scripted.
The scripts shall be run before committing the code. And the same scripts shall be executed in automated
CI/CD pipelines. That approach allows to make identical build and test actions across the entire delivery
pipeline. And have a clear separation between developer and DevOps roles (developers are responsible
for individual components, their build, test and packaging. DevOps are responsible for running CI/CD pipelines, assembling and testing entire system from individual components).

**Multiple communication protocols:** The library contains clients that allow to connect to the microservice several different ways, depending on the environment or client requirements. For instance: on-premises the microservice can be deployed as a docker container. Locally it can be called via GRPC interface and externally via REST. Moreover, several microservice can be packaged into a single process, essentially represending a monolith. In that scenario, then can be called using in-process calls using the DirectClient.

**Monitoring and Observability:** All clients are instrumented to collect logs of called operations, metrics that collect number of calls, average call times and number of erors, and traces. Depending on the deployment configuration that information can be sent to different destinations: console, Promethous, DataDog service, ApplicationInsights, CloudWatch and others.

**Versioning:** Data objects and clients are versioned from the beginning. When breaking changes are introduced into the microservice, it shall keep the old version of the interface for backward-compatibility and expose a new version of the interface simultaniously. Then client library will have a new set of objects and clients for the new version, while keeping the old one intact. That will provide a clear versioning and backward-compatibility for users of the microservice.

<a name="links"></a> Quick links:

* Communication Protocols:
  - [gRPC Version 1](src/protos/entities_v1.proto)
  - [HTTP Version 1](src/swagger/entities_v1.yaml)
* [Microservice](https://github.com/pip-templates-services/pip-service-data-dotnet)
* [API Reference](https://pip-templates-services.github.io/pip-client-data-dotnet/index.html)
* [Change Log](CHANGELOG.md)


## Contract

The contract of the microservice is presented below. 

```cs
    [DataContract]
    public class EntityV1 : IStringIdentifiable, ICloneable
    {
        public EntityV1() { }

        [DataMember(Name = "id")]
        public string Id { get; set; } // Entity ID
        [DataMember(Name = "site_id")]
        public string SiteId { get; set; } // ID of a work site (field installation)
        [DataMember(Name = "type")]
        public string Type { get; set; } // Entity type: Type2, Type1 or Type3
        [DataMember(Name = "name")]
        public string Name { get; set; } // Human readable name
        [DataMember(Name = "content")]
        public string Content { get; set; } // String content
    }

    public interface IEntitiesClientV1
    {
        Task<DataPage<EntityV1>> GetEntitiesAsync(string correlationId, FilterParams filter, PagingParams paging);

        Task<EntityV1> GetEntityByIdAsync(string correlationId, string entityId);

        Task<EntityV1> GetEntityByNameAsync(string correlationId, string name);

        Task<EntityV1> CreateEntityAsync(string correlationId, EntityV1 entity);

        Task<EntityV1> UpdateEntityAsync(string correlationId, EntityV1 entity);

        Task<EntityV1> DeleteEntityByIdAsync(string correlationId, string entityId);
    }

```

## Get

Get the client library source from GitHub:
```bash
git clone git@github.com:pip-templates-services/pip-client-data-dotnet.git
```

Install the client library as a binary dependency:
```bash
dotnet add package PipTemplatesClientData
```

## Use

Install the client NuGet package as
```bash
dotnet add package PipTemplatesClientData
```

Inside your code get the reference to the client SDK
```cs
using PipServices3.Commons.Config;
using PipServices3.Commons.Data;

using PipTemplatesClientData.Clients.Version1;
using PipTemplatesServiceData.Data.Version1;
```

Instantiate the client
```cs
// Create the client instance
var client = new EntitiesRestClientV1();
```

Define client connection parameters
```cs
// Client configuration
var httpConfig = ConfigParams.FromTuples(
	"connection.protocol", "http",
	"connection.host", "localhost",
	"connection.port", 8080
);
// Configure the client
client.Configure(httpConfig);
```

Connect to the microservice
```cs
// Connect to the microservice
await client.OpenAsync(null);
    
// Work with the microservice
...
```

Call the microservice using the client API
```cs
// Define a entity
var ENTITY1 = new EntityV1
{
    Id = "1",
    SiteId = "1",
    Type = EntityTypeV1.Type1,
    Name = "00001",
    Content = "ABC"
};

// Create the entity
var entity = await client.CreateEntityAsync(null, ENTITY1);

// Do something with the returned entity...

// Get a list of entities
var page = client.GetEntitiesAsync(null, 
    FilterParams.FromTuples("name", "TestEntity"),
    new PagingParams(0, 10)
);

// Do something with the returned page...
// E.g. entity = page.data[0];
```

## Develop

For development you shall install the following prerequisites:
* Core .NET SDK 3.1+
* Visual Studio Code or another IDE of your choice
* Docker

Restore dependencies:
```bash
dotnet restore src/src.csproj
```

Compile the code:
```bash
dotnet build src/src.csproj
```

Before running tests launch infrastructure services and required microservices:
```bash
docker-compose -f ./docker-compose.dev.yml up
```

Run automated tests:
```bash
dotnet restore test/test.csproj
dotnet test test/test.csproj
```

Generate GRPC protobuf stubs:
```bash
./protogen.ps1
```

Generate API documentation:
```bash
./docgen.ps1
```

Before committing changes run dockerized build and test as:
```bash
./build.ps1
./test.ps1
./clear.ps1
```

## Contacts

This microservice was created and currently maintained by *Sergey Seroukhov* and *Danil Prisyzhniy*.
