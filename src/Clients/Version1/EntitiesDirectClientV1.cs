
using System.Collections.Generic;
using System.Threading.Tasks;
using PipServices3.Commons.Convert;
using PipServices3.Commons.Data;
using PipServices3.Commons.Refer;
using PipServices3.Rpc.Clients;
using PipTemplatesClientData.Data.Version1;
using ServiceEntityV1 = PipTemplatesServiceData.Data.Version1.EntityV1;


namespace PipTemplatesClientData.Clients.Version1
{
    public class EntitiesDirectClientV1 : DirectClient<dynamic>, IEntitiesClientV1
    {

        public EntitiesDirectClientV1() : base()
        {
            this._dependencyResolver.Put("controller", new Descriptor("pip-service-data", "controller", "*", "*", "1.0"));
        }

        private ServiceEntityV1 ToServiceEntity(EntityV1 entity)
        {
            // convert entity to service entity type
            return JsonConverter.FromJson<ServiceEntityV1>(JsonConverter.ToJson(entity));
        }

        private EntityV1 FromServiceEntity(ServiceEntityV1 entity)
        {
            // convert service entity to entity type
            return JsonConverter.FromJson<EntityV1>(JsonConverter.ToJson(entity));
        }

        public async Task<EntityV1> CreateEntityAsync(string correlationId, EntityV1 entity)
        {
            var timing = this.Instrument(correlationId, "entities.create_entity");
            try
            {
                entity = FromServiceEntity(await this._controller.CreateEntityAsync(correlationId, ToServiceEntity(entity)));
                return entity;
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
                var entity = FromServiceEntity(await this._controller.DeleteEntityByIdAsync(correlationId, entityId));
                return entity;
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
                DataPage<EntityV1> result = new DataPage<EntityV1>(data: new List<EntityV1>());
                DataPage<ServiceEntityV1> servicePageResult = await this._controller.GetEntitiesAsync(correlationId, filter, paging);

                result.Total = servicePageResult.Total;
                servicePageResult.Data.ForEach((item) => { 
                    result.Data.Add(FromServiceEntity(item)); 
                });

                return result;
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
                var entity = FromServiceEntity(await this._controller.GetEntityByIdAsync(correlationId, entityId));
                return entity;
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
                var entity = FromServiceEntity(await this._controller.GetEntityByNameAsync(correlationId, name));
                return entity;
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
                entity = FromServiceEntity(await this._controller.UpdateEntityAsync(correlationId, ToServiceEntity(entity)));
                return entity;
            }
            finally
            {
                timing.EndTiming();
            }
        }
    }
}
