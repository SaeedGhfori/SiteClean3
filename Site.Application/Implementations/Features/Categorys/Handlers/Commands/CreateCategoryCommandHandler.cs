using FluentValidation;
using MediatR;
using Site.Application.Definitions.Contracts.Repositories;
using Site.Application.Definitions.Dtos.Categorys;
using Site.Application.Definitions.Dtos.Invoices;
using Site.Application.Implementations.Features.Categorys.Requests.Commands;
using Site.Application.Implementations.Validators.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Implementations.Features.Categorys.Handlers.Commands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper mapper;

        public CreateCategoryCommandHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }
        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<CategoryDto>();

            var category = mapper.Map<Category>(request.CategoryDto);
            category = await _repository.Add(category);

            var categoryDtoCreated = mapper.Map<CategoryDto>(category);
            response.Success = true;
            response.Message = "Category created successfully";
            response.Data = categoryDtoCreated;
            return response.Data.Id;
        }
    }
}
