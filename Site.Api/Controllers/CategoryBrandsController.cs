using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Site.Application.Definitions.Dtos.Categorys;
using Site.Application.Implementations.Features.CategoryBrands.Requests.Commands;
using Site.Application.Implementations.Features.CategoryBrands.Requests.Queries;
using Site.Application.Implementations.Features.Categorys.Requests.Commands;
using Site.Application.Implementations.Features.Categorys.Requests.Queries;

namespace Site.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryBrandsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryBrandsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryBrandDto>> GetById(int id)
        {
            var categoryBrand = await _mediator.Send(new GetCategoryBrandDetailRequest { Id = id });
            if (categoryBrand == null)
            {
                return NotFound();
            }
            return Ok(categoryBrand);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryBrandDto>>> GetAll()
        {
            var categoryBrands = await _mediator.Send(new GetCategoryBrandListRequest());
            return Ok(categoryBrands);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryBrandDto>> Create([FromBody] CategoryBrandDto categoryBrandDto)
        {
            if (categoryBrandDto == null)
            {
                return BadRequest();
            }
            var createdCategoryBrand = await _mediator.Send(new CreateCategoryBrandCommand { CategoryBrandDto = categoryBrandDto });
            return CreatedAtAction(nameof(GetById), new { id = createdCategoryBrand }, createdCategoryBrand);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CategoryBrandDto categoryBrandDto)
        {
            //if (id != categoryBrandDto.Id)
            //{
            //    return BadRequest("CategoryBrand ID mismatch");
            //}

            var categoryBrandToUpdate = await _mediator.Send(new GetCategoryBrandDetailRequest { Id = id });
            if (categoryBrandToUpdate == null)
            {
                return NotFound($"CategoryBrand with Id = {id} not found");
            }

            await _mediator.Send(new UpdateCategoryBrandCommand { CategoryBrandDto = categoryBrandDto });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var categoryBrandToDelete = await _mediator.Send(new GetCategoryBrandDetailRequest { Id = id });
            if (categoryBrandToDelete == null)
            {
                return NotFound($"CategoryBrand with Id = {id} not found");
            }

            await _mediator.Send(new RemoveCategoryBrandCommand { Id = id });
            return NoContent();
        }
    }

}