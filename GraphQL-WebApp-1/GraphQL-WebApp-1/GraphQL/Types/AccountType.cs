using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using GraphQLWebApp1.Entities;

namespace GraphQLWebApp1.GraphQL.Types
{
    public class AccountType:ObjectGraphType<Account>
    {
        public AccountType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id propertity from accounts");
            Field(x => x.Description).Description("Description property from the account object.");
            Field(x => x.OwnerId, type: typeof(IdGraphType)).Description("OwnerId property from the account object.");
            Field<AccountTypeEnumType>("Type", "Enumeration for the account type object.");            
        }
    }
}
