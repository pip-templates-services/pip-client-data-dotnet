using System.Collections.Generic;
using System.Threading.Tasks;

using PipServices3.Commons.Data;
using PipTemplatesServiceData.Data.Version1;
using PipTemplatesServiceData.Logic;

namespace PipTemplatesClientData.Clients.Version1
{
    public class EntitiesNullClientV1 : IEntitiesClientV1
    {
        public async Task<EntityV1> CreateEntityAsync(string correlationId, EntityV1 entity)
        {
            return await Task.FromResult<EntityV1>(null);
        }

        public async Task<EntityV1> DeleteEntityByIdAsync(string correlationId, string entityId)
        {
            return await Task.FromResult<EntityV1>(null);
        }

        public async Task<DataPage<EntityV1>> GetEntitiesAsync(string correlationId, FilterParams filter, PagingParams paging)
        {
            return await Task.FromResult(new DataPage<EntityV1>(new List<EntityV1>(), 0));
        }

        public async Task<EntityV1> GetEntityByIdAsync(string correlationId, string entityId)
        {
            return await Task.FromResult<EntityV1>(null);
        }

        public async Task<EntityV1> GetEntityByNameAsync(string correlationId, string name)
        {
            return await Task.FromResult<EntityV1>(null);
        }

        public async Task<EntityV1> UpdateEntityAsync(string correlationId, EntityV1 entity)
        {
            return await Task.FromResult<EntityV1>(null);
        }
    }
}
