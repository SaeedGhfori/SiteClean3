using MediatR;
using Site.Application.Definitions.Contracts.Repositories;
using Site.Application.Implementations.Features.Invoices.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Implementations.Features.Invoices.Handlers.Commands
{
    public class RemoveInvoiceCommandHandler : IRequestHandler<RemoveInvoiceCommand, Unit>
    {
        private readonly IInvoiceRepository _repository;
        private readonly IMapper mapper;

        public RemoveInvoiceCommandHandler(IInvoiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(RemoveInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await _repository.GetById(request.Id);
            if (invoice == null)
            {
                throw new NotFoundException(nameof(InvoiceDto), request.Id);
            }
            await _repository.Remove(invoice);
            return Unit.Value;
        }
    }
}
