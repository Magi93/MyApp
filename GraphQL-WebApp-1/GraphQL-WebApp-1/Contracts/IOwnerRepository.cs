using GraphQLWebApp1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLWebApp1.Contracts
{
    public interface IOwnerRepository
    {
        Task<IEnumerable<Owner>> GetAll();
        Owner GetById(Guid id);
    }
}
