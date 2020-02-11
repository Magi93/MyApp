using GraphQL;
using GraphQL.Types;
using GraphQLWebApp1.Contracts;
using GraphQLWebApp1.Entities;
using GraphQLWebApp1.GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLWebApp1.GraphQL
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(IOwnerRepository ownerRepository)
        {
            Field<ListGraphType<OwnerType>>("Owners", resolve: context => ownerRepository.GetAll());
            //Field<OwnerType>("Owner",
            //    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "ownerId" }),
            //    resolve: context =>
            //    {
            //    var id = context.GetArgument<Guid>("OwnerId");
            //    return ownerRepository.GetById(id);
            //    });
            Field<OwnerType>("Owner",
              arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "ownerId" }),
              resolve: context =>
              {
                  Guid id;
                  if (!Guid.TryParse(context.GetArgument<string>("ownerId"),out id))
                  {
                      context.Errors.Add(new ExecutionError("Wrong value of guid"));//Error handling
                      return null;
                  }
                  return ownerRepository.GetById(id);
              });
        }
    }
}
