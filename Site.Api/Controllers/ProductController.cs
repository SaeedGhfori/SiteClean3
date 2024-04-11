using MediatR;
using Microsoft.AspNetCore.Mvc;
using Site.Application.Definitions.Models.Products;
using Site.Infrastructure.Services.Products.Requests.Commands;
using Site.Infrastructure.Services.Products.Requests.Queries;

namespace Site.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _mediator.Send(new GetProductListRequest());
            if (products == null || !products.Any())
            {
                return NotFound("No products found.");
            }
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ArgumentNullException());
            }

            var product = await _mediator.Send(new GetProductDetailRequest { Id = id });
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            if (product == null || product.Price == null)
            {
                return BadRequest(new ArgumentNullException());
            }

            var createdProduct = await _mediator.Send(new CreateProductCommand { Product = product });
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id <= 0 || product == null || product.Price == null)
            {
                return BadRequest(new ArgumentNullException());
            }

            var updatedProduct = await _mediator.Send(new UpdateProductCommand { Product = product });
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ArgumentNullException());
            }
            await _mediator.Send(new RemoveProductCommand { Id = id });
            return NoContent();
        }
    }
}
