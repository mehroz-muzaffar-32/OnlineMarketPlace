using Torico.Shared.Models;
using Torico.Shared.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using Torico.Client.Services.Abstracts;
using Torico.Client.Services.Interfaces;

namespace Torico.Client.Services.Implementations;

public class ProductService : ClientService, IProductService
{
    public ProductService(HttpClient httpClient) : base(httpClient) { }

    protected override string GetBaseUrl() => BuildBaseUrl("product");

    public async Task<ServiceResponse> AddProduct(Product model)
    {
        return await PerformPostRequestWithResponse("", model);
    }

    public async Task<List<Product>> GetAllProducts(bool isFeatured)
    {
        var queryParams = new Dictionary<string, string>
        {
            { "featured", isFeatured.ToString().ToLower() }
        };

        return await PerformGetRequestWithListResponse<Product>("", queryParams);
    }
}