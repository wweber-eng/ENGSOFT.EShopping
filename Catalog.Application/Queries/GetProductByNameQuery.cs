using Catalog.Application.Responses;
using Catalog.Core.Entities;
using MediatR;
using System.Resources;
using System.Security.Principal;

namespace Catalog.Application.Queries
{
    public class GetProductByNameQuery : IRequest<List<ProductResponse>>
    {
        public string Name { get; set; }
        public GetProductByNameQuery(string name)
        {
            Name = name;
        }
    }
}
