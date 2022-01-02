using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Mongo.DataAccess.DataContext;
using Moq;

namespace Mongo.DataAccess.Test
{
  public  class BaseMongoTestDbSetting
    {

        public Mock<IcontractDatabaseSetting> _moqIcontractDatabaseSettings = new Mock<IcontractDatabaseSetting>();

        public ContractDatabaseSetting ContractDatabaseSetting = new ContractDatabaseSetting()
        {
            ConnectionSetting = "mongodb://localhost:27017",
            DatabaseName = "TestDb"
        };

        public BaseMongoTestDbSetting()
        {
            _moqIcontractDatabaseSettings.Setup(x => x.ConnectionSetting).Returns(ContractDatabaseSetting.ConnectionSetting);
            _moqIcontractDatabaseSettings.Setup(x => x.DatabaseName).Returns(ContractDatabaseSetting.DatabaseName);
        }
    }
}
