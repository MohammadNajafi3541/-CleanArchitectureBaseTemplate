using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Mongo.DataAccess.DataContext
{
   public class ContractDatabaseSetting : IcontractDatabaseSetting
    {
        public string ContractCollectionName { get; set; }
        public string ConnectionSetting { get; set; }
        public string DatabaseName { get; set; }
    }
}
