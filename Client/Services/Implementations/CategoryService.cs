using Torico.Shared.Models;
using Torico.Shared.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using Torico.Client.Services.Abstracts;
using Torico.Client.Services.Interfaces;

namespace Torico.Client.Services.Implementations;

public class CategoryService : ClientService, ICategoryService
{
    public CategoryService(HttpClient httpClient) : base(httpClient) { }

    protected override string GetBaseUrl() => BuildBaseUrl("category");

    public async Task<ServiceResponse> AddCategory(Category model)
    {
        return await PerformPostRequestWithResponse("", model);
    }

    public async Task<List<Category>> GetAllCategories()
    {
        var queryParams = new Dictionary<string, string>();
        return await PerformGetRequestWithListResponse<Category>("", queryParams);
    }
}