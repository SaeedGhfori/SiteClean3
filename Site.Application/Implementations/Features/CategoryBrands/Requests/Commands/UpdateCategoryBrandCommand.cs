using MediatR;
using Site.Application.Definitions.Dtos.Categorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Implementations.Features.CategoryBrands.Requests.Commands
{
    public class UpdateCategoryBrandCommand : IRequest<Unit>
    {
        public CategoryBrandDto CategoryBrandDto { get; set; }

    }
}
