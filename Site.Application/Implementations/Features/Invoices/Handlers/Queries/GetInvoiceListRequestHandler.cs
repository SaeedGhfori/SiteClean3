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
    public class GetInvoiceListRequestHandler : IRequestHandler<GetInvoiceListRequest, List<InvoiceDto>>
    {
        private readonly IInvoiceRepository _repository;
        private readonly IMapper mapper;

        public GetInvoiceListRequestHandler(IInvoiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }
        public async Task<List<InvoiceDto>> Handle(GetInvoiceListRequest request, CancellationToken cancellationToken)
        {
            var invoices = await _repository.GetAll(i => i.Id, false);
            return mapper.Map<List<InvoiceDto>>(invoices);
        }
    }
}
