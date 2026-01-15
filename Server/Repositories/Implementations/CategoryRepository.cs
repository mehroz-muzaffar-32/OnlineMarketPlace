using Microsoft.EntityFrameworkCore;
using Torico.Server.Data;
using Torico.Server.Repositories.Interfaces;
using Torico.Shared.Models;
using Torico.Shared.Responses;
using Torico.Shared.Utils;
using Torico.Server.Repositories.Abstracts;

namespace Torico.Server.Repositories.Implementations;

public class CategoryRepository : BaseRepository, ICategoryRepository
{
    public CategoryRepository(AppDbContext appDbContext) : base(appDbContext) { }

    public async Task<ServiceResponse> AddCategory(Category model)
    {
        if (model is null) return new ServiceResponse(false, "Model is null");
        var response = await CheckName<Category>(model.Name!);
        var isSuccessful = response.IsSuccessful;
        var message = response.Message;
        if (isSuccessful)
        {
            GetCurrentScope<Category>().Add(model);
            await Commit();
            return new ServiceResponse(true, "Category Saved");
        }
        return new ServiceResponse(isSuccessful, message);
    }

    public async Task<List<Category>> GetAllCategories() => await GetCurrentScope<Category>().ToListAsync();

    public async Task<ServiceResponse> UpdateCategory(int id, Category updatedCategory)
    {
        var category = await GetCurrentScope<Category>().FindAsync(id);
        if (category == null)
        {
            return new ServiceResponse(false, "Category not found.");
        }

        var nameValidationResponse = await CheckName<Category>(updatedCategory.Name!);
        if (!nameValidationResponse.IsSuccessful && category.Name != updatedCategory.Name)
        {
            return nameValidationResponse;
        }

        category.Name = updatedCategory.Name;
        await Commit();

        return new ServiceResponse(true, "Category updated successfully.");
    }

    public async Task<ServiceResponse> DeleteCategory(int id)
    {
        var category = await GetCurrentScope<Category>().FindAsync(id);
        if (category == null)
        {
            return new ServiceResponse(false, "Category not found.");
        }

        GetCurrentScope<Category>().Remove(category);
        await Commit();

        return new ServiceResponse(true, "Category deleted successfully.");
    }

    public async Task<Category?> GetCategoryById(int id)
    {
        return await GetCurrentScope<Category>().FindAsync(id);
    }
}