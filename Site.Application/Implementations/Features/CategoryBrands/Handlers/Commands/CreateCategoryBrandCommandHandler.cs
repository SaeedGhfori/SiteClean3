using AutoMapper;
using FluentValidation;
using MediatR;
using Site.Application.Definitions.Contracts.Repositories;
using Site.Application.Definitions.Dtos.Categorys;
using Site.Application.Definitions.Dtos.Invoices;
using Site.Application.Implementations.Features.CategoryBrands.Requests.Commands;
using Site.Application.Implementations.Validators.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Implementations.Features.CategoryBrands.Handlers.Commands
{
    public class CreateCategoryBrandCommandHandler : IRequestHandler<CreateCategoryBrandCommand, int>
    {
        private readonly ICategoryBrandRepository _repository;
        private readonly IMapper mapper;

        public CreateCategoryBrandCommandHandler(ICategoryBrandRepository repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }

        public async Task<int> Handle(CreateCategoryBrandCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<CategoryBrandDto>();

            var categoryBrand = mapper.Map<CategoryBrand>(request.CategoryBrandDto);
            categoryBrand = await _repository.Add(categoryBrand);

            var categoryBrandDtoCreated = mapper.Map<CategoryBrandDto>(categoryBrand);
            response.Success = true;
            response.Message = "CategoryBrand created successfully";
            response.Data = categoryBrandDtoCreated;
            return response.Data.Id;
        }
    }
}

