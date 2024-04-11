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
    public class GetCategoryDetailRequestHandler : IRequestHandler<GetCategoryDetailRequest, CategoryDto>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper mapper;

        public GetCategoryDetailRequestHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }
        public async Task<CategoryDto> Handle(GetCategoryDetailRequest request, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetById(request.Id);
            return mapper.Map<CategoryDto>(categories);
        }
    }
}
