using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure.Mongo.DataAccess.DataContext
{
    public class ContractMongoDbBaseDataContext
    {
        public IMongoCollection<Contract> _contract;
        public IMongoCollection<Person> _account;

        public ContractMongoDbBaseDataContext(IcontractDatabaseSetting databaseSetting)
        {
            DatabaseMapper.MongoDbModelMapper();
                
            var client = new MongoClient(databaseSetting.ConnectionSetting);
            var database = client.GetDatabase(databaseSetting.DatabaseName);

            _contract = database.GetCollection<Contract>("Contracts");
            _account = database.GetCollection<Person>("Accounts");


            
        }
    }
}
