using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using MongoDB.Bson.Serialization;

namespace Infrastructure.Mongo.DataAccess.DataContext
{
    public class DatabaseMapper
    {

        public static void MongoDbModelMapper()
        {
            if(!BsonClassMap.IsClassMapRegistered(typeof(Contract)))
            {
                BsonClassMap.RegisterClassMap<Contract>(cm =>
                {

                    cm.AutoMap();
                    cm.MapIdMember(c => c.Id);
                    //  cm.SetDiscriminator(nameof(Contract));
                    cm.MapMember(x => x.CustomerName).SetElementName("Name");
                });  

            }
        }
    }
}
