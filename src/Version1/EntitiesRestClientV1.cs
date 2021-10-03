using System;
using System.Net.Http;
using System.Threading.Tasks;

using PipServices3.Commons.Config;
using PipServices3.Commons.Data;
using PipServices3.Rpc.Clients;
using PipTemplatesClientData.Data.Version1;

namespace PipTemplatesClientData.Clients.Version1
{
    public class EntitiesRestClientV1 : RestClient, IEntitiesClientV1
    {
        public EntitiesRestClientV1(dynamic config = null) : base()
        {
            this._baseRoute = "v1/entities";

            if (config != null)
                this.Configure(ConfigParams.FromValue(config));
        }

        public override void Configure(ConfigParams config)
        {
            base.Configure(config);
        }

        public async Task<EntityV1> CreateEntityAsync(string correlationId, EntityV1 entity)
        {
            var timing = this.Instrument(correlationId, "v1/entities.create_entity");
            try
            {
                return await this.CallAsync<EntityV1>(correlationId, HttpMethod.Post, "/entities", entity);
            }
            finally
            {
                timing.EndTiming();
            }
        }

        public async Task<EntityV1> DeleteEntityByIdAsync(string correlationId, string entityId)
        {
            var timing = this.Instrument(correlationId, "v1/entities.delete_entity_by_id");
            try
            {
                return await this.CallAsync<EntityV1>(correlationId, HttpMethod.Delete, "/entities/" + entityId);
            }
            finally
            {
                timing.EndTiming();
            }
        }

        public async Task<DataPage<EntityV1>> GetEntitiesAsync(string correlationId, FilterParams filter, PagingParams paging)
        {
            var timing = this.Instrument(correlationId, "v1/entities.get_entities");
            try
            {
                return await this.CallAsync<DataPage<EntityV1>>(correlationId,
                    HttpMethod.Get,
                    "/entities",
                    new
                    {
                        filter = filter,
                        paging = paging
                    }
                );
            }
            finally
            {
                timing.EndTiming();
            }
        }

        public async Task<EntityV1> GetEntityByIdAsync(string correlationId, string entityId)
        {
            var timing = this.Instrument(correlationId, "v1/entities.get_entity_by_id");
            try
            {
                return await this.CallAsync<EntityV1>(correlationId, HttpMethod.Get, "/entities/" + entityId);
            }
            finally
            {
                timing.EndTiming();
            }
        }

        public async Task<EntityV1> GetEntityByNameAsync(string correlationId, string name)
        {
            var timing = this.Instrument(correlationId, "v1/entities.get_entity_by_name");
            try
            {
                return await this.CallAsync<EntityV1>(correlationId, HttpMethod.Get, "/entities/name/" + name);
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
                return await this.CallAsync<EntityV1>(
                    correlationId,
                    HttpMethod.Put,
                    "/entities",
                    entity
                );
            }
            finally
            {
                timing.EndTiming();
            }
        }
    }
}
