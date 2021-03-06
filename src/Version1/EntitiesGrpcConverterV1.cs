using System;
using System.Collections;
using System.Collections.Generic;

using PipServices3.Commons.Errors;
using PipServices3.Commons.Data;
using ApplicationException = PipServices3.Commons.Errors.ApplicationException;
using Newtonsoft.Json;
using PipTemplatesClientData.Data.Version1;

namespace PipTemplatesClientData.Clients.Version1
{
    public class EntitiesGrpcConverterV1
    {

        public static EntitiesV1.ErrorDescription FromError(Exception err)
        {
            if (err == null) return null;

            var description = ErrorDescriptionFactory.Create(err);
            var obj = new EntitiesV1.ErrorDescription();

            obj.Type = description.Type != null ? description.Type : "default";
            obj.Category = description.Category != null ? description.Category : "default";
            obj.Code = description.Code != null ? description.Code : "default";
            obj.CorrelationId = description.CorrelationId != null ? description.CorrelationId : "default";
            obj.Status = description.Status.ToString() != null ? description.Status.ToString() : "default";
            obj.Message = description.Message != null ? description.Message : "default";
            obj.Cause = description.Cause != null ? description.Cause : "default";
            obj.StackTrace = description.StackTrace != null ? description.StackTrace : "default";

            EntitiesGrpcConverterV1.SetMap(obj.Details, description.Details);

            return obj;
        }

        public static ApplicationException ToError(EntitiesV1.ErrorDescription obj)
        {
            if (obj == null || (obj.Category == "" && obj.Message == ""))
                return null;

            var description = new ErrorDescription
            {
                Type = obj.Type,
                Category = obj.Category,
                Code = obj.Code,
                CorrelationId = obj.CorrelationId,
                Status = int.Parse(obj.Status),
                Message = obj.Message,
                Cause = obj.Cause,
                StackTrace = obj.StackTrace,
                Details = (StringValueMap)EntitiesGrpcConverterV1.GetMap(obj.Details)
            };

            return ApplicationExceptionFactory.Create(description);
        }

        public static void SetMap(object map, object values)
        {
            if (values == null) return;

            if (values is IList)
            {
                foreach (var entry in (IList)values)
                {
                    if (entry is IList)
                        (map as IList)[0] = (entry as IList)[1];
                }
            }
            else
            {
                if (map is IDictionary)
                {
                    foreach (var propName in (values as IDictionary).Keys)
                    {
                        if ((values as IDictionary).Contains(propName))
                        {
                            (map as IDictionary).Add(propName, (values as IDictionary)[propName]);
                        }
                    }
                }
            }
        }

        public static object GetMap(object map)
        {
            var values = new Dictionary<string, string>();
            EntitiesGrpcConverterV1.SetMap(values, map);
            return values;
        }

        private static string ToJson(object value)
        {
            if (value == null || (string)value == "") return null;
            return JsonConvert.SerializeObject(value);
        }

        private static object FromJson(string value)
        {
            if (value == null || (string)value == "") return null;
            return JsonConvert.DeserializeObject<EntityV1>(value);
        }

        public static EntitiesV1.PagingParams FromPagingParams(PagingParams paging)
        {
            if (paging == null) return null;

            var obj = new EntitiesV1.PagingParams
            {
                Skip = paging.Skip!=null ? (long)paging.Skip: default,
                Take = paging.Take != null ? (int)paging.Take: default,
                Total = paging.Total ? paging.Total : default
            };

            return obj;
        }

        public static PagingParams ToPagingParams(EntitiesV1.PagingParams obj)
        {
            if (obj == null)
                return null;

            var paging = new PagingParams();

            paging.Total = obj.Total;

            if (obj.Skip != 0)
                paging.Skip = obj.Skip;
            if (obj.Take != 0)
                paging.Take = obj.Skip;

            return paging;
        }

        public static EntitiesV1.Entity FromEntity(EntityV1 entity)
        {
            if (entity == null) return null;

            var obj = new EntitiesV1.Entity
            {
                Id = entity.Id,
                SiteId = entity.SiteId,
                Type = entity.Type,
                Name = entity.Name,
                Content = entity.Content
            };

            return obj;
        }

        public static EntityV1 ToEntity(EntitiesV1.Entity obj)
        {
            if (obj == null) return null;

            var entity = new EntityV1
            {
                Id = obj.Id,
                SiteId = obj.SiteId,
                Type = obj.Type,
                Name = obj.Name,
                Content = obj.Content
            };

            return entity;
        }

        public static EntitiesV1.EntitiesPage FromEntitiesPage(DataPage<EntityV1> page)
        {
            if (page == null) return null;

            var obj = new EntitiesV1.EntitiesPage
            {
                Total = page.Total != null ? (long)page.Total : default,
            };

            page.Data.ForEach((item) =>
            {
                obj.Data.Add(FromEntity(item));
            });

            return obj;
        }

        public static DataPage<EntityV1> ToEntitiesPage(EntitiesV1.EntitiesPage obj)
        {
            if (obj == null) return null;

            var data = new List<EntityV1>();

            foreach (var item in obj.Data)
                data.Add(ToEntity(item));

            var page = new DataPage<EntityV1>
            {
                Total = obj.Total,
                Data = data
            };

            return page;
        }
    }
}