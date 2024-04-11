using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using RestSharp;
using Site.Application.Definitions.Contracts.Repositories;
using Site.Application.Definitions.Models.Products;
using Site.Infrastructure.Helpers.RestSharp;
using Site.Infrastructure.Services.Products.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Infrastructure.Services.Products.Handlers.Queries
{
    public class GetProductListRequestHandler : IRequestHandler<GetProductListRequest, List<Product>>
    {
        private readonly IMapper mapper;
        private readonly IRestClient<Product> _restClient;
        private readonly string _productBaseUrl;

        public GetProductListRequestHandler(IMapper mapper, IRestClient<Product> restClient, IConfiguration configuration)
        {
            this.mapper = mapper;
            _restClient = restClient;
            _productBaseUrl = configuration.GetValue<string>("RestClientSettings:ProductBaseUrl");
        }
        public async Task<List<Product>> Handle(GetProductListRequest request, CancellationToken cancellationToken)
        {
            var url = $"Product";

            try
            {
                var result = await _restClient.GetCollectionAsync(_productBaseUrl + url);
                return result ?? new List<Product>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
