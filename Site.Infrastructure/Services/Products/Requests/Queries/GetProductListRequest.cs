using Site.Application.Definitions.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Infrastructure.Services.Products.Requests.Queries
{
    public class GetProductListRequest : IRequest<List<Product>>
    {

    }
}
