using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Site.Application.Definitions.Contracts.Repositories;
using Site.Application.Definitions.Dtos.Invoices;
using Site.Application.Definitions.Models.Products;
using Site.Infrastructure.Helpers.RestSharp;
using Site.Infrastructure.Services.Products.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Infrastructure.Services.Products.Handlers.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IMapper mapper;
        private readonly IRestClient<Product> _restClient;
        private readonly string _productBaseUrl;

        public UpdateProductCommandHandler(IMapper mapper, IRestClient<Product> restClient, IConfiguration configuration)
        {
            this.mapper = mapper;
            _restClient = restClient;
            _productBaseUrl = configuration.GetValue<string>("RestClientSettings:ProductBaseUrl");
        }
        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            if (request.Product.Price == null)
            {
                throw new ArgumentNullException();
            }

            var url = $"Product/{request.Product.Id}";

            try
            {
                var result = await _restClient.PutAsync(_productBaseUrl, url, request.Product);
                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
