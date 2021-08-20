using System.Threading.Tasks;
using Xunit;

using PipTemplatesClientData.Clients.Version1;
using PipServices3.Commons.Data;
using PipTemplatesServiceData.Data.Version1;

namespace PipTemplates.Client.Data.Test.Version1
{
    public class EntitiesClientV1Fixture
    {
        private static EntityV1 ENTITY1 = new EntityV1
        {
            Id = "1",
            Name = "00001",
            Type = EntityTypeV1.Type1,
            SiteId = "1",
            Content = "ABC"
        };

        private static EntityV1 ENTITY2 = new EntityV1
        {
            Id = "2",
            Name = "00002",
            Type = EntityTypeV1.Type2,
            SiteId = "1",
            Content = "XYZ"
        };

        private IEntitiesClientV1 _client;
        private string correlationId;

        public EntitiesClientV1Fixture(IEntitiesClientV1 client)
        {
            correlationId = IdGenerator.NextLong();

            Assert.NotNull(client);
            this._client = client;
        }

        public async Task TestCrudOperations()
        {
            // Create the first entity
            var entity = await this._client.CreateEntityAsync(correlationId, ENTITY1);
            Assert.NotNull(entity);
            Assert.Equal(ENTITY1.Name, entity.Name);
            Assert.Equal(ENTITY1.SiteId, entity.SiteId);
            Assert.Equal(ENTITY1.Type, entity.Type);
            Assert.Equal(ENTITY1.Name, entity.Name);
            Assert.NotNull(entity.Content);

            // Create the second entity
            entity = await this._client.CreateEntityAsync(correlationId, ENTITY2);
            Assert.NotNull(entity);
            Assert.Equal(ENTITY2.Name, entity.Name);
            Assert.Equal(ENTITY2.SiteId, entity.SiteId);
            Assert.Equal(ENTITY2.Type, entity.Type);
            Assert.Equal(ENTITY2.Name, entity.Name);
            Assert.NotNull(entity.Content);

            // Get all entities
            var page = await this._client.GetEntitiesAsync(
                correlationId,
                new FilterParams(),
                new PagingParams()
            );
            Assert.NotNull(page);
            Assert.Equal(2, page.Data.Count);

            var entity1 = page.Data[0];

            // Update the entity
            entity1.Name = "ABC";

            entity = await this._client.UpdateEntityAsync(correlationId, entity1);
            Assert.NotNull(entity);
            Assert.Equal(entity1.Id, entity.Id);
            Assert.Equal("ABC", entity.Name);

            // Get entity by name
            entity = await this._client.GetEntityByNameAsync(correlationId, entity1.Name);
            Assert.NotNull(entity);
            Assert.Equal(entity1.Id, entity.Id);

            // Delete the entity
            entity = await this._client.DeleteEntityByIdAsync(correlationId, entity1.Id);
            Assert.NotNull(entity);
            Assert.Equal(entity1.Id, entity.Id);

            // Try to get deleted entity
            entity = await this._client.GetEntityByIdAsync(correlationId, entity1.Id);
            Assert.Null(entity);

        }
    }
}
