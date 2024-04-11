using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Infrastructure.Services.Products.Requests.Commands
{
    public class RemoveProductCommand : IRequest<Unit>
    {
        public int Id { get; set; }

    }
}
