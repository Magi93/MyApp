using GraphQLWebApp1.Entities;
using GraphQLWebApp1.Entities.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLWebApp1.Contracts
{   
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAllAccountsPerOwner(Guid ownerId);
        Task<ILookup<Guid, Account>> GetAccountByOwnerIds(IEnumerable<Guid> ownerIds);
    }
}
