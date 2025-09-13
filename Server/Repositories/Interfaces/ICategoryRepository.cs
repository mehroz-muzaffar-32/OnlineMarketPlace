using Microsoft.EntityFrameworkCore;
using Torico.Server.Data;
using Torico.Shared.Models;
using Torico.Shared.Responses;

namespace Torico.Server.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<ServiceResponse> AddCategory(Category model);
    Task<List<Category>> GetAllCategories();
    Task<Category?> GetCategoryById(int id);
    Task<ServiceResponse> UpdateCategory(int id, Category updatedCategory);
    Task<ServiceResponse> DeleteCategory(int id);
}
