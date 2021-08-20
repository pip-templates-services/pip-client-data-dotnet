

using PipServices3.Commons.Config;
using PipServices3.Commons.Data;
using PipTemplatesClientData.Clients.Version1;
using PipTemplatesServiceData.Data.Version1;

using System;
using System.Collections.Generic;
using System.Text;

namespace Program
{
    public class Class1
    {
        public void test()
        {
            var w = new ConfigParams();
            var a = new FilterParams();
            var c = new PagingParams();
            var q = new EntityV1();
            var qq = new EntityTypeV1();
            var client = new EntitiesRestClientV1();

            var entity = new EntityV1
            {
                Id = "1",
                SiteId = "1",
                Type = EntityTypeV1.Type1,
                Name = "00001",
                Content = "ABC"
            };

            var entity = client.GetEntitiesAsync(null, 
                FilterParams.FromTuples("name", "TestEntity"),
                new PagingParams(0, 10)
            );
        }
    }
}
