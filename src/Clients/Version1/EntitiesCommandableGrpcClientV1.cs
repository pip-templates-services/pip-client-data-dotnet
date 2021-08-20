using PipServices3.Commons.Config;
using PipServices3.Commons.Data;
using PipServices3.Grpc.Clients;
using PipTemplatesServiceData.Data.Version1;
using System.Threading.Tasks;

namespace PipTemplatesClientData.Clients.Version1
{
    public class EntitiesCommandableGrpcClientV1 : CommandableGrpcClient, IEntitiesClientV1
    {
        public EntitiesCommandableGrpcClientV1(object config = null) : base("v1.entities")
        {
            if (config != null)
                this.Configure(ConfigParams.FromValue(config));
        }


        public async Task<EntityV1> CreateEntityAsync(string correlationId, EntityV1 entity)
        {
            return await this.CallCommandAsync<EntityV1>("create_entity",
                correlationId,
                new { entity = entity }
            );
        }

        public async Task<EntityV1> DeleteEntityByIdAsync(string correlationId, string entityId)
        {
            return await this.CallCommandAsync<EntityV1>("delete_entity_by_id",
                correlationId,
                new { entity_id = entityId }
            );
        }

        public async Task<DataPage<EntityV1>> GetEntitiesAsync(string correlationId, FilterParams filter, PagingParams paging)
        {
            return await this.CallCommandAsync<DataPage<EntityV1>>("get_entities",
                correlationId,
                new { filter = filter, paging = paging }
            );
        }

        public async Task<EntityV1> GetEntityByIdAsync(string correlationId, string entityId)
        {
            return await this.CallCommandAsync<EntityV1>("get_entity_by_id",
                correlationId,
                new { entity_id = entityId }
            );
        }

        public async Task<EntityV1> GetEntityByNameAsync(string correlationId, string name)
        {
            return await this.CallCommandAsync<EntityV1>("get_entity_by_name",
                correlationId,
                new { name = name }
            );
        }

        public async Task<EntityV1> UpdateEntityAsync(string correlationId, EntityV1 entity)
        {
            return await this.CallCommandAsync<EntityV1>("update_entity",
                correlationId,
                new { entity = entity }
            );
        }
    }
}
