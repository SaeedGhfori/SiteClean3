using MediatR;
using Site.Application.Definitions.Contracts.Repositories;
using Site.Application.Definitions.Dtos.Categorys;
using Site.Application.Definitions.Dtos.Invoices;
using Site.Application.Implementations.Features.CategoryBrands.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Implementations.Features.CategoryBrands.Handlers.Commands
{
    public class UpdateCategoryBrandCommandHandler : IRequestHandler<UpdateCategoryBrandCommand, Unit>
    {
        private readonly ICategoryBrandRepository _repository;
        private readonly IMapper mapper;

        public UpdateCategoryBrandCommandHandler(ICategoryBrandRepository repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateCategoryBrandCommand request, CancellationToken cancellationToken)
        {
            var categoryBrand = mapper.Map<CategoryBrand>(request.CategoryBrandDto);
            await _repository.Update(categoryBrand);

            return Unit.Value;
        }
    }
}
