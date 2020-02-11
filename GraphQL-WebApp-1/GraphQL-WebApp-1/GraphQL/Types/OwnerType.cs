using GraphQL.DataLoader;
using GraphQL.Types;
using GraphQLWebApp1.Contracts;
using GraphQLWebApp1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLWebApp1.GraphQL.Types
{
    public class OwnerType : ObjectGraphType<Owner>
    {
        public OwnerType(IAccountRepository accountRepository, IDataLoaderContextAccessor dataLoaderContextAccessor)
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id property from the owner object.");
            Field(x => x.Name).Description("Name property from the owner object.");
            Field(x => x.Address).Description("Address property from the owner object.");
           // Field<ListGraphType<AccountType>>("accounts", resolve: context => accountRepository.GetAllAccountsPerOwner(context.Source.Id));
            Field<ListGraphType<AccountType>>("accounts", resolve: context =>
            {
                var Loader = dataLoaderContextAccessor.Context.GetOrAddCollectionBatchLoader<Guid, Account>("GetAccountsByOwnerIds", accountRepository.GetAccountByOwnerIds);
                return Loader.LoadAsync(context.Source.Id);
            });
        }
    }
}
