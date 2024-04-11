using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Implementations.Features.Categorys.Requests.Commands
{
    public class CreateCategoryCommand : IRequest<int>
    {
        public CategoryDto CategoryDto { get; set; }
    }
}
