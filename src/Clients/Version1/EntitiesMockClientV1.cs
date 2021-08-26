
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using PipServices3.Commons.Data;
using PipTemplatesClientData.Data.Version1;

namespace PipTemplatesClientData.Clients.Version1
{
    public class EntitiesMockClientV1 : IEntitiesClientV1
    {
        private int _maxPageSize = 100;
        private List<EntityV1> _items;

        public EntitiesMockClientV1(List<EntityV1> items = null)
        {
            this._items = items ?? new List<EntityV1>();
        }

        private Func<EntityV1, bool> ComposeFilter(FilterParams filter)
        {
            filter = filter != null ? filter : new FilterParams();

            var id = filter.GetAsNullableString("id");
            var siteId = filter.GetAsNullableString("site_id");
            var name = filter.GetAsNullableString("name");

            var tempNames = filter.GetAsNullableString("names");
            var names = tempNames != null ? new List<string>(tempNames.Split(",")) : null;

            return (EntityV1 item) =>
            {
                if (id != null && item.Id != id) return false;
                if (siteId != null && item.SiteId != siteId) return false;
                if (name != null && item.Name != name) return false;
                if (names != null && names.IndexOf(item.Name) < 0) return false;
                return true;
            };
        }

        public async Task<EntityV1> CreateEntityAsync(string correlationId, EntityV1 entity)
        {
            if (entity == null)
                return null;


            entity = (EntityV1)entity.Clone();

            entity.Id = entity.Id != null && entity.Id != "" ? entity.Id : IdGenerator.NextLong();

            this._items.Add(entity);

            return await Task.FromResult(entity);
        }

        public async Task<EntityV1> DeleteEntityByIdAsync(string correlationId, string entityId)
        {
            var entity = this._items.Find(item => item.Id == entityId);

            if (entity == null)
                return await Task.FromResult<EntityV1>(null);

            this._items.Remove(entity);
            return await Task.FromResult<EntityV1>(entity);
        }

        public async Task<DataPage<EntityV1>> GetEntitiesAsync(string correlationId, FilterParams filter, PagingParams paging)
        {
            var filterEntities = this.ComposeFilter(filter);
            var entities = this._items.FindAll((e) => filterEntities(e));

            // Extract a page
            paging = paging != null ? paging : new PagingParams();
            var skip = paging.GetSkip(-1);
            var take = paging.GetTake(this._maxPageSize);
            int? total = null;

            if (paging.Total)
                total = entities.Count;
            if (skip > 0)
                entities.RemoveRange(0, (int)skip);

            entities = entities.Count > take ? entities.GetRange(0, (int)take) : entities;

            return await Task.FromResult(new DataPage<EntityV1>(entities, total));

        }

        public async Task<EntityV1> GetEntityByIdAsync(string correlationId, string entityId)
        {
            var entities = this._items.FindAll((x) => x.Id == entityId);

            var entity = entities.Count > 0 ? entities[0] : null;
            return await Task.FromResult(entity);
        }

        public async Task<EntityV1> GetEntityByNameAsync(string correlationId, string name)
        {
            var entities = this._items.FindAll((x) => x.Name == name);

            var entity = entities.Count > 0 ? entities[0] : null;
            return await Task.FromResult(entity);
        }

        public async Task<EntityV1> UpdateEntityAsync(string correlationId, EntityV1 entity)
        {

            var index = this._items.FindIndex(item => item.Id == entity.Id);

            if (index < 0)
                return await Task.FromResult<EntityV1>(null);


            entity = (EntityV1)entity.Clone();
            this._items[index] = entity;

            return await Task.FromResult(entity);
        }
    }
}
