
using System.Threading.Tasks;

using EntitiesV1;

using PipServices3.Commons.Data;
using PipServices3.Grpc.Clients;
using PipTemplatesServiceData.Data.Version1;

namespace PipTemplatesClientData.Clients.Version1
{
    public class EntitiesGrpcClientV1 : GrpcClient, IEntitiesClientV1
    {

        public EntitiesGrpcClientV1() : this("entities_v1") { }

        public EntitiesGrpcClientV1(string name) : base(name) { }

        public async Task<EntityV1> CreateEntityAsync(string correlationId, EntityV1 entity)
        {
            var request = new EntitiesV1.EntityRequest();
            var gprcEntity = EntitiesGrpcConverterV1.FromEntity(entity);
            request.Entity = gprcEntity;

            var timing = this.Instrument(correlationId, "v1.entities.create_entity");

            try
            {
                var response = await this.CallAsync<EntityRequest, EntityReply>("create_entity", request);

                if (response != null && response.Error != null)
                    throw EntitiesGrpcConverterV1.ToError(response.Error);

                var result = response != null
                    ? EntitiesGrpcConverterV1.ToEntity(response.Entity)
                    : null;
                return result;
            }
            finally
            {
                timing.EndTiming();
            }
        }

        public async Task<EntityV1> DeleteEntityByIdAsync(string correlationId, string entityId)
        {
            var request = new EntitiesV1.EntityIdRequest();
            request.EntityId = entityId;

            var timing = this.Instrument(correlationId, "v1.entities.delete_entity_by_id");

            try
            {
                var response = await this.CallAsync<EntityIdRequest, EntityReply>("delete_entity_by_id", request);

                if (response != null && response.Error != null)
                    throw EntitiesGrpcConverterV1.ToError(response.Error);

                var result = response != null
                    ? EntitiesGrpcConverterV1.ToEntity(response.Entity)
                    : null;
                return result;

            }
            finally
            {
                timing.EndTiming();
            }
        }

        public async Task<DataPage<EntityV1>> GetEntitiesAsync(string correlationId, FilterParams filter, PipServices3.Commons.Data.PagingParams paging)
        {

            var request = new EntitiesV1.EntitiesPageRequest();

            EntitiesGrpcConverterV1.SetMap(request.Filter, filter);

            request.Paging = EntitiesGrpcConverterV1.FromPagingParams(paging);

            var timitng = this.Instrument(correlationId, "v1.entities.get_entities");

            try
            {
                var response = await this.CallAsync<EntitiesPageRequest, EntitiesPageReply>("get_entities", request);

                if (response != null && response.Error != null)
                    throw EntitiesGrpcConverterV1.ToError(response.Error);

                var result = response != null
                    ? EntitiesGrpcConverterV1.ToEntitiesPage(response.Page)
                    : null;

                return result;
            }
            finally
            {
                timitng.EndTiming();
            }
        }

        public async Task<EntityV1> GetEntityByIdAsync(string correlationId, string entityId)
        {
            var request = new EntitiesV1.EntityIdRequest();

            request.EntityId = entityId;

            var timing = this.Instrument(correlationId, "v1.entities.get_entity_by_id");

            try
            {
                var response = await this.CallAsync<EntityIdRequest, EntityReply>("get_entity_by_id", request);

                if (response != null && response.Error != null)
                    throw EntitiesGrpcConverterV1.ToError(response.Error);

                var result = response != null
                    ? EntitiesGrpcConverterV1.ToEntity(response.Entity)
                    : null;

                return result;
            }
            finally
            {
                timing.EndTiming();
            }
        }

        public async Task<EntityV1> GetEntityByNameAsync(string correlationId, string name)
        {
            var request = new EntitiesV1.EntityNameRequest();
            request.Name = name;

            var timing = this.Instrument(correlationId, "v1.entities.get_entity_by_name");

            try
            {
                var response = await this.CallAsync<EntityNameRequest, EntityReply>("get_entity_by_name", request);

                if (response != null && response.Error != null)
                    throw EntitiesGrpcConverterV1.ToError(response.Error);

                var result = response != null
                    ? EntitiesGrpcConverterV1.ToEntity(response.Entity)
                    : null;
                return result;
            }
            finally
            {
                timing.EndTiming();
            }
        }

        public async Task<EntityV1> UpdateEntityAsync(string correlationId, EntityV1 entity)
        {
            var request = new EntitiesV1.EntityRequest();
            var gprcEntity = EntitiesGrpcConverterV1.FromEntity(entity);
            request.Entity = gprcEntity;

            var timing = this.Instrument(correlationId, "v1.entities.update_entity");

            try
            {
                var response = await this.CallAsync<EntityRequest, EntityReply>("update_entity", request);

                if (response != null && response.Error != null)
                    throw EntitiesGrpcConverterV1.ToError(response.Error);

                var result = response != null
                    ? EntitiesGrpcConverterV1.ToEntity(response.Entity)
                    : null;
                return result;
            }
            finally
            {
                timing.EndTiming();
            }
        }
    }
}
