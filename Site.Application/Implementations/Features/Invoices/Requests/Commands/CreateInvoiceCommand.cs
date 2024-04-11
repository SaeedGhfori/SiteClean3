using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Implementations.Features.Invoices.Requests.Commands
{
    public class CreateInvoiceCommand : IRequest<int>
    {
        public InvoiceDto InvoiceDto { get; set; }
    }
}
