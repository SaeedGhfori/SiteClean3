using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Implementations.Features.CategoryBrands.Requests.Commands
{
    public class RemoveCategoryBrandCommand : IRequest<Unit>
    {
        public int Id { get; set; }

    }
}
