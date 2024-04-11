using MediatR;
using Site.Application.Definitions.Contracts.Repositories;
using Site.Application.Implementations.Features.CategoryBrands.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Implementations.Features.CategoryBrands.Handlers.Queries
{
    public class GetCategoryBrandDetailRequestHandler : IRequestHandler<GetCategoryBrandDetailRequest, CategoryBrandDto>
    {
        private readonly ICategoryBrandRepository _repository;
        private readonly IMapper mapper;

        public GetCategoryBrandDetailRequestHandler(ICategoryBrandRepository repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }
        public async Task<CategoryBrandDto> Handle(GetCategoryBrandDetailRequest request, CancellationToken cancellationToken)
        {
            var categoryBrand = await _repository.GetById(request.Id);
            return mapper.Map<CategoryBrandDto>(categoryBrand);
        }
    }
}
