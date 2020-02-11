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
    public class OwnerRepository :IOwnerRepository
    {
        ApplicationContext _context;
        public OwnerRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Owner>> GetAll()//this is also correct
        {
            return await _context.Owners.ToListAsync().ConfigureAwait(true);
        }
        //public IEnumerable<Owner> GetAll()=> _context.Owners.ToList();
       public Owner GetById(Guid id)
        {
            return _context.Owners.SingleOrDefault(o => o.Id.Equals(id));
        }

    }
}
