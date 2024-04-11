using FluentValidation;
using MediatR;
using Site.Application.Definitions.Contracts.Repositories;
using Site.Application.Definitions.Dtos.Invoices;
using Site.Application.Implementations.Features.Invoices.Requests.Commands;
using Site.Application.Implementations.Validators.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Implementations.Features.Invoices.Handlers.Commands
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, int>
    {
        private readonly IInvoiceRepository _repository;
        private readonly IMapper mapper;

        public CreateInvoiceCommandHandler(IInvoiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }
        public async Task<int> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<InvoiceDto>();
            var validationResult = await new InvoiceDtoValidator().ValidateAsync(request.InvoiceDto);
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                throw new ValidationExceptionApp(validationResult);
                //return response.Data.Id;
            }

            var invoice = mapper.Map<Invoice>(request.InvoiceDto);
            invoice = await _repository.Add(invoice);

            var invoiceDtoCreated = mapper.Map<InvoiceDto>(invoice);
            response.Success = true;
            response.Message = "Invoice created successfully";
            response.Data = invoiceDtoCreated;
            return response.Data.Id;
        }
    }
}
