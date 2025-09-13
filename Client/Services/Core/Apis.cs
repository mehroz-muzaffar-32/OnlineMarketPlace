using Torico.Shared.Models;
using Torico.Client.Services.Abstracts;
using Torico.Client.Services.Implementations;
using Torico.Client.Services.Interfaces;

namespace Torico.Client.Services.Core;

public class Apis
{
    private readonly HttpClient _httpClient;

    public Apis(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public IProductService ProductService => new ProductService(_httpClient);
    public ICategoryService CategoryService => new CategoryService(_httpClient);
}
