using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Implementations.Features.Invoices.Requests.Queries
{
    public class GetInvoiceListRequest : IRequest<List<InvoiceDto>>
    {

    }
}
