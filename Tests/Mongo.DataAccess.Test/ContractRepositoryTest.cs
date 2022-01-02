using System;
using Domain.Entities;
using Infrastructure.Mongo.DataAccess.Repositories;
using Xunit;

namespace Mongo.DataAccess.Test
{
    public class ContractRepositoryTest : BaseMongoTestDbSetting
    {
        private ContractRepository _contractRepository;

        private Contract contractEntity = new Contract()
        {
            BrokerAgentName = "BrokerAgentName",
            CustomerAddress = "CustomerAddress",
            CustomerName = "CustomerName"

        };

        public ContractRepositoryTest()
        {
            _contractRepository = new ContractRepository(_moqIcontractDatabaseSettings.Object);
        }


        [Fact]
        public  async void ShouldReturnContractModelWithRequestedValueAndId()
        {
            var result = await _contractRepository.AddAsync(contractEntity);

            Assert.False(string.IsNullOrEmpty(result.Id));
            Assert.NotNull(result);
            
        }
        

        [Fact]
        public async void ShouldReturnArgumentWithNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _contractRepository.AddAsync(null));
        }


    }
}
