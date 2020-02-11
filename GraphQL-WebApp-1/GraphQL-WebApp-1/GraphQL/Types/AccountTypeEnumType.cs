using GraphQL.Types;
using GraphQLWebApp1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLWebApp1.GraphQL.Types
{
    public class AccountTypeEnumType:EnumerationGraphType<TypeOfAccount>
    {
        public AccountTypeEnumType()
        {
            Name = "Type";
            Description = "Enumeration for the account type object";
        }

    }
}
