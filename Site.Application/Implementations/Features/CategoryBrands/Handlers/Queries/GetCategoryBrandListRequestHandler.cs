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
    public class GetCategoryBrandListRequestHandler : IRequestHandler<GetCategoryBrandListRequest, List<CategoryBrandDto>>
    {
        private readonly ICategoryBrandRepository _repository;
        private readonly IMapper mapper;

        public GetCategoryBrandListRequestHandler(ICategoryBrandRepository repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }
        public async Task<List<CategoryBrandDto>> Handle(GetCategoryBrandListRequest request, CancellationToken cancellationToken)
        {
            var invoices = await _repository.GetAll(i => i.Id, false);
            return mapper.Map<List<CategoryBrandDto>>(invoices);
        }
    }
}
