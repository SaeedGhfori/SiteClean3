using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Site.Application.Definitions.Dtos.Categorys;
using Site.Application.Implementations.Features.Categorys.Requests.Commands;
using Site.Application.Implementations.Features.Categorys.Requests.Queries;

namespace Site.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var category = await _mediator.Send(new GetCategoryDetailRequest { Id = id });
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
        {
            var categories = await _mediator.Send(new GetCategoryListRequest());
            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create(CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest();
            }

            var createdCategory = await _mediator.Send(new CreateCategoryCommand { CategoryDto = categoryDto });
            return CreatedAtAction(nameof(GetById), new { id = createdCategory }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CategoryDto categoryDto)
        {
            //if (id != categoryDto.Id)
            //{
            //    return BadRequest("Category ID mismatch");
            //}

            var categoryToUpdate = await _mediator.Send(new GetCategoryDetailRequest { Id = id });
            if (categoryToUpdate == null)
            {
                return NotFound($"Category with Id = {id} not found");
            }

            await _mediator.Send(new UpdateCategoryCommand { CategoryDto = categoryDto });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var categoryToDelete = await _mediator.Send(new GetCategoryDetailRequest { Id = id });
            if (categoryToDelete == null)
            {
                return NotFound($"Category with Id = {id} not found");
            }

            await _mediator.Send(new RemoveCategoryCommand { Id = id });
            return NoContent();
        }
    }
}