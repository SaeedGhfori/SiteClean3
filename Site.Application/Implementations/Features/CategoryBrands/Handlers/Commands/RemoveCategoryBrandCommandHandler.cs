using MediatR;
using Site.Application.Definitions.Contracts.Repositories;
using Site.Application.Implementations.Features.CategoryBrands.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Implementations.Features.CategoryBrands.Handlers.Commands
{
    public class RemoveCategoryBrandCommandHandler : IRequestHandler<RemoveCategoryBrandCommand, Unit>
    {
        private readonly ICategoryBrandRepository _repository;
        private readonly IMapper mapper;

        public RemoveCategoryBrandCommandHandler(ICategoryBrandRepository repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(RemoveCategoryBrandCommand request, CancellationToken cancellationToken)
        {
            var CategoryBrand = await _repository.GetById(request.Id);
            if (CategoryBrand == null)
            {
                throw new NotFoundException(nameof(CategoryBrandDto), request.Id);
            }
            await _repository.Remove(CategoryBrand);
            return Unit.Value;
        }
    }
}
