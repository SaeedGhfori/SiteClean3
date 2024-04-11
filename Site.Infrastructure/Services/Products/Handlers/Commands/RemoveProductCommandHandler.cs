using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Site.Application.Definitions.Contracts.Repositories;
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
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand, Unit>
    {
        private readonly IMapper mapper;
        private readonly IRestClient<Product> _restClient;
        private readonly string _productBaseUrl;

        public RemoveProductCommandHandler(IMapper mapper, IRestClient<Product> restClient, IConfiguration configuration)
        {
            this.mapper = mapper;
            _restClient = restClient;
            _productBaseUrl = configuration.GetValue<string>("RestClientSettings:ProductBaseUrl");
        }
        public async Task<Unit> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {

            if (request.Id == 0)
            {
                throw new ArgumentNullException();
            }

            var url = $"Product/{request.Id}";

            try
            {
                await _restClient.DeleteAsync(_productBaseUrl + url);
                return Unit.Value;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
