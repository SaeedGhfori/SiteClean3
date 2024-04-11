using MediatR;
using Site.Application.Definitions.Contracts.Repositories;
using Site.Application.Definitions.Dtos.Categorys;
using Site.Application.Definitions.Dtos.Invoices;
using Site.Application.Implementations.Features.Categorys.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Implementations.Features.Categorys.Handlers.Commands
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper mapper;

        public UpdateCategoryCommandHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categories = mapper.Map<Category>(request.CategoryDto);
            await _repository.Update(categories);

            return Unit.Value;
        }
    }
}
