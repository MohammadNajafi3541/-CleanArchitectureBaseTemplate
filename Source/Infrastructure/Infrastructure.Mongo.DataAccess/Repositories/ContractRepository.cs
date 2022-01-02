using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Mongo.DataAccess.DataContext;
using MongoDB.Driver;

namespace Infrastructure.Mongo.DataAccess.Repositories
{
    public class ContractRepository : ContractMongoDbBaseDataContext, IContractRepository
    {
        public ContractRepository(IcontractDatabaseSetting databaseSetting):base(databaseSetting)
        {

        }
        public async Task<Contract> AddAsync(Contract contract)
        {
            if (contract == null)
                throw new ArgumentNullException(nameof(contract));

            await _contract.InsertOneAsync(contract);

            return contract;
        }

        public  async Task<Contract> FindAsync(string id)
        {
            var result = await _contract.FindAsync(c => c.Id == id);

            return result.FirstOrDefault();
        }

        public  List<Contract> GetAll(PagingParameter requestParameter)
        {
            var result = _contract.Find(x => true).Skip((requestParameter.PageNumber - 1) * requestParameter.PageSize).Limit(requestParameter.PageSize);
            return result.ToList();

        }

        public async Task<List<Contract>> GetAllAsync()
        {
            var result = await _contract.FindAsync(c => true);

            return result.ToList();
        }

        public async Task<bool> UpdateAsync(Contract contract)
        {
            if (contract == null)
                throw new ArgumentNullException(nameof(contract));

            await _contract.ReplaceOneAsync(item => item.Id == contract.Id, contract, new ReplaceOptions { IsUpsert = true });

            return true;
        }
    }
}
