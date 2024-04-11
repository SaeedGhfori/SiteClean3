using MediatR;
using Site.Application.Definitions.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Infrastructure.Services.Products.Requests.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public Product Product { get; set; }
    }
}
