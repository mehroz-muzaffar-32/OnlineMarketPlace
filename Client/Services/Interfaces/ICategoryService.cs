using Torico.Shared.Models;
using Torico.Shared.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Torico.Client.Services.Interfaces;

public interface ICategoryService
{
    Task<ServiceResponse> AddCategory(Category model);
    Task<List<Category>> GetAllCategories();
}