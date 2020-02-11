using GraphQLWebApp1.Contracts;
using GraphQLWebApp1.Entities;
using GraphQLWebApp1.Entities.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLWebApp1.Repository
{
    public class AccountRepository:IAccountRepository
    {
        ApplicationContext _context;
        public AccountRepository(ApplicationContext context)
        {
            _context = context;
        }        

        public IEnumerable<Account> GetAllAccountsPerOwner(Guid ownerId) => _context.Accounts
            .Where(a => a.OwnerId.Equals(ownerId))
            .ToList();
        public async Task<ILookup<Guid,Account>> GetAccountByOwnerIds(IEnumerable<Guid> ownerIds)
        {
            var accounts = await _context.Accounts.Where(o => ownerIds.Contains(o.OwnerId)).ToListAsync().ConfigureAwait(true);
            return accounts.ToLookup(x => x.OwnerId);
        }
    }
}
