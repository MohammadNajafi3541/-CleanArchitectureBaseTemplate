using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
   public interface IContractRepository
    {

        Task<List<Contract>> GetAllAsync();
        List<Contract> GetAll(PagingParameter requestParameter);
        Task<Contract> FindAsync(string id);
        Task<Contract> AddAsync(Contract contract);
        Task<bool> UpdateAsync(Contract contract);




    }
}
