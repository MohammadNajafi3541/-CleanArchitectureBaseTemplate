using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Mongo.DataAccess.DataContext
{
  public  interface IcontractDatabaseSetting
    {
        string ContractCollectionName { get; set; }

        string  ConnectionSetting { get; set; }

        string  DatabaseName { get; set; }
    }
}
