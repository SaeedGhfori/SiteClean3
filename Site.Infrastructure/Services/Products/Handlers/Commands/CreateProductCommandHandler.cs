using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Site.Application.Definitions.Contracts.Repositories;
using Site.Application.Definitions.Dtos.Invoices;
using Site.Application.Definitions.Models.Products;
using Site.Application.Implementations.Validators.Invoices;
using Site.Infrastructure.Helpers.RestSharp;
using Site.Infrastructure.Services.Products.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Infrastructure.Services.Products.Handlers.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IMapper mapper;
        private readonly IRestClient<Product> _restClient;
        private readonly string _productBaseUrl;

        public CreateProductCommandHandler(IMapper mapper, IRestClient<Product> restClient, IConfiguration configuration)
        {
            this.mapper = mapper;
            _restClient = restClient;
            _productBaseUrl = configuration.GetValue<string>("RestClientSettings:ProductBaseUrl");
        }
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (request.Product.Price == null)
            {
                throw new ArgumentNullException();
            }

            var url = $"Product";

            try
            {
                var result = await _restClient.PostAsync(_productBaseUrl, url, request.Product);
                return result.Id;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
