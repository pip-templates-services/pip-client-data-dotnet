
using System.Threading.Tasks;

using PipServices3.Commons.Data;
using PipServices3.Commons.Refer;
using PipServices3.Rpc.Clients;
using PipTemplatesServiceData.Data.Version1;
using PipTemplatesServiceData.Logic;

namespace PipTemplatesClientData.Clients.Version1
{
    public class EntitiesDirectClientV1 : DirectClient<IEntitiesController>, IEntitiesClientV1
    {

        public EntitiesDirectClientV1() : base()
        {
            this._dependencyResolver.Put("controller", new Descriptor("pip-service-data", "controller", "*", "*", "1.0"));
        }

        public async Task<EntityV1> CreateEntityAsync(string correlationId, EntityV1 entity)
        {
            var timing = this.Instrument(correlationId, "entities.create_entity");
            try
            {
                return await this._controller.CreateEntityAsync(correlationId, entity);
            }
            finally
            {
                timing.EndTiming();
            }
        }

        public async Task<EntityV1> DeleteEntityByIdAsync(string correlationId, string entityId)
        {
            var timing = this.Instrument(correlationId, "entities.delete_entity_by_id");
            try
            {
                return await this._controller.DeleteEntityByIdAsync(correlationId, entityId);
            }
            finally
            {
                timing.EndTiming();
            }
        }

        public async Task<DataPage<EntityV1>> GetEntitiesAsync(string correlationId, FilterParams filter, PagingParams paging)
        {
            var timing = this.Instrument(correlationId, "entities.get_entities");
            try
            {
                return await this._controller.GetEntitiesAsync(correlationId, filter, paging);
            }
            finally
            {
                timing.EndTiming();
            }

        }

        public async Task<EntityV1> GetEntityByIdAsync(string correlationId, string entityId)
        {
            var timing = this.Instrument(correlationId, "entities.get_entity_by_id");
            try
            {
                return await this._controller.GetEntityByIdAsync(correlationId, entityId);
            }
            finally
            {
                timing.EndTiming();
            }
        }

        public async Task<EntityV1> GetEntityByNameAsync(string correlationId, string name)
        {
            var timing = this.Instrument(correlationId, "entities.get_entity_by_name");
            try
            {
                return await this._controller.GetEntityByNameAsync(correlationId, name);
            }
            finally
            {
                timing.EndTiming();
            }
        }

        public async Task<EntityV1> UpdateEntityAsync(string correlationId, EntityV1 entity)
        {
            var timing = this.Instrument(correlationId, "entities.update_entity");
            try
            {
                return await this._controller.UpdateEntityAsync(correlationId, entity);
            }
            finally
            {
                timing.EndTiming();
            }
        }
    }
}
