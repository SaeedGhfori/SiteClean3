using MediatR;
using Site.Application.Definitions.Contracts.Repositories;
using Site.Application.Implementations.Features.Invoices.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Implementations.Features.Invoices.Handlers.Queries
{
    public class GetInvoiceDetailRequestHandler : IRequestHandler<GetInvoiceDetailRequest, InvoiceDto>
    {
        private readonly IInvoiceRepository _repository;
        private readonly IMapper mapper;

        public GetInvoiceDetailRequestHandler(IInvoiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }
        public async Task<InvoiceDto> Handle(GetInvoiceDetailRequest request, CancellationToken cancellationToken)
        {
            var categoryBrand = await _repository.GetById(request.Id);
            return mapper.Map<InvoiceDto>(categoryBrand);
        }
    }
}
