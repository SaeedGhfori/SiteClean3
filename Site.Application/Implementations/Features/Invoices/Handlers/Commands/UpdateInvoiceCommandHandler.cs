using MediatR;
using Site.Application.Definitions.Contracts.Repositories;
using Site.Application.Definitions.Dtos.Invoices;
using Site.Application.Implementations.Features.Invoices.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Implementations.Features.Invoices.Handlers.Commands
{
    public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, Unit>
    {
        private readonly IInvoiceRepository _repository;
        private readonly IMapper mapper;

        public UpdateInvoiceCommandHandler(IInvoiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoices = mapper.Map<Invoice>(request.InvoiceDto);
            await _repository.Update(invoices);

            return Unit.Value;
        }
    }
}
