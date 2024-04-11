using MediatR;
using Site.Application.Definitions.Contracts.Repositories;
using Site.Application.Implementations.Features.Categorys.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Implementations.Features.Categorys.Handlers.Queries
{
    public class GetCategoryListRequestHandler : IRequestHandler<GetCategoryListRequest, List<CategoryDto>>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper mapper;

        public GetCategoryListRequestHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }
        public async Task<List<CategoryDto>> Handle(GetCategoryListRequest request, CancellationToken cancellationToken)
        {
            var invoices = await _repository.GetAll(i => i.Id, false);
            return mapper.Map<List<CategoryDto>>(invoices);
        }
    }
}
