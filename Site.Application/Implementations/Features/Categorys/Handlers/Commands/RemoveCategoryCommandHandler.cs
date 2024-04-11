using MediatR;
using Site.Application.Definitions.Contracts.Repositories;
using Site.Application.Implementations.Features.Categorys.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Implementations.Features.Categorys.Handlers.Commands
{
    public class RemoveCategoryCommandHandler : IRequestHandler<RemoveCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper mapper;

        public RemoveCategoryCommandHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
        {
            var Category = await _repository.GetById(request.Id);
            if (Category != null)
            {
                throw new NotFoundException(nameof(InvoiceDto), request.Id);
            }
            await _repository.Remove(Category);
            return Unit.Value;
        }
    }
}
