using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Site.Application.Definitions.Dtos.Categorys;
using Site.Application.Definitions.Dtos.Invoices;
using Site.Application.Implementations.Features.Categorys.Requests.Commands;
using Site.Application.Implementations.Features.Categorys.Requests.Queries;
using Site.Application.Implementations.Features.Invoices.Requests.Commands;
using Site.Application.Implementations.Features.Invoices.Requests.Queries;

namespace Site.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper mapper;
        public InvoicesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var invoice = await _mediator.Send(new GetInvoiceDetailRequest { Id = id });
            if (invoice == null) return NotFound();
            return Ok(invoice);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var invoices = await _mediator.Send(new GetInvoiceListRequest());
            return Ok(invoices);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(InvoiceDto invoiceDto)
        {
            if (invoiceDto == null)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(new CreateInvoiceCommand { InvoiceDto = invoiceDto });
            return CreatedAtAction(nameof(GetByIdAsync), new { id = response }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, InvoiceDto invoiceDto)
        {
            //if (id != invoiceDto.Id)
            //{
            //    return BadRequest("Identifier mismatch")
            //}

            var categoryToUpdate = await _mediator.Send(new GetInvoiceDetailRequest { Id = id });
            if (categoryToUpdate == null)
            {
                return NotFound($"Category with Id = {id} not found");
            }

            await _mediator.Send(new UpdateInvoiceCommand { InvoiceDto = invoiceDto });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var invoiceToDelete = await _mediator.Send(new GetInvoiceDetailRequest { Id = id });
            if (invoiceToDelete == null)
            {
                return NotFound($"Category with Id = {id} not found");
            }

            await _mediator.Send(new RemoveInvoiceCommand { Id = id });
            return NoContent();
        }

        [HttpHead("{id}")]
        public async Task<IActionResult> HeadAsync(int id)
        {
            var invoice = await _mediator.Send(new GetInvoiceDetailRequest { Id = id });
            if (invoice == null) return NotFound();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync(int id, JsonPatchDocument<InvoiceDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest("Patch document is empty");

            var invoiceFromDb = await _mediator.Send(new GetInvoiceDetailRequest { Id = id });
            if (invoiceFromDb == null) return NotFound();

            var invoiceToPatch = mapper.Map<InvoiceDto>(invoiceFromDb);

            patchDoc.ApplyTo(invoiceToPatch, error =>
            {
                ModelState.AddModelError(error.Operation.path, error.ErrorMessage);
            });

            if (!ModelState.IsValid) return BadRequest(ModelState);

            mapper.Map(invoiceToPatch, invoiceFromDb);
            await _mediator.Send(new UpdateInvoiceCommand { InvoiceDto = invoiceFromDb });
            return NoContent();
        }

    }
}
