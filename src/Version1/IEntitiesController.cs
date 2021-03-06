using System.Threading.Tasks;

using PipTemplatesClientData.Data.Version1;
using PipServices3.Commons.Data;

namespace PipTemplatesClientData.Clients.Version1
{
    public interface IEntitiesController
    {
        Task<DataPage<EntityV1>> GetEntitiesAsync(string correlationId, FilterParams filter, PagingParams paging);
        Task<EntityV1> GetEntityByIdAsync(string correlationId, string entityId);
        Task<EntityV1> GetEntityByNameAsync(string correlationId, string entityId);
        Task<EntityV1> CreateEntityAsync(string correlationId, EntityV1 entity);
        Task<EntityV1> UpdateEntityAsync(string correlationId, EntityV1 entity);
        Task<EntityV1> DeleteEntityByIdAsync(string correlationId, string entityId);
    }
}
