using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
   public interface IPersonRepository
    {

        Task<List<Person>> GetAllAsync();
        List<Person> GetAll(PagingParameter requestParameter);
        Task<Person> FindAsync(string id);
        Task<Person> AddAsync(Person contract);
        Task<bool> UpdateAsync(Person contract);




    }
}
