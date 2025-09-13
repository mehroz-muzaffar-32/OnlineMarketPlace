using Microsoft.EntityFrameworkCore;
using Torico.Server.Data;
using Torico.Server.Repositories.Interfaces;
using Torico.Shared.Models;
using Torico.Shared.Responses;
using Torico.Server.Repositories.Abstracts;

namespace Torico.Server.Repositories.Implementations;

public class ProductRepository : BaseRepository, IProductRepository
{
    public ProductRepository(AppDbContext appDbContext) : base(appDbContext) { }

    public async Task<ServiceResponse> AddProduct(Product model)
    {
        if (model is null) return new ServiceResponse(false, "Model is null");
        var response = await CheckName<Product>(model.Name!);
        var isSuccessful = response.IsSuccessful;
        var message = response.Message;
        if (isSuccessful)
        {
            GetCurrentScope<Product>().Add(model);
            await Commit();
            return new ServiceResponse(true, "Product Saved");
        }
        return new ServiceResponse(isSuccessful, message);
    }

    public async Task<List<Product>> GetAllProducts(bool featured)
    {
        if (featured)
            return await GetCurrentScope<Product>().Where(_ => _.Featured).ToListAsync();
        else
            return await GetCurrentScope<Product>().ToListAsync();
    }

    public async Task<Product?> GetProductById(int id)
    {
        return await GetCurrentScope<Product>().FindAsync(id);
    }

    public async Task<ServiceResponse> UpdateProduct(int id, Product updatedProduct)
    {
        var product = await GetCurrentScope<Product>().FindAsync(id);
        if (product == null)
        {
            return new ServiceResponse(false, "Product not found.");
        }

        var nameValidationResponse = await CheckName<Product>(updatedProduct.Name!);
        if (!nameValidationResponse.IsSuccessful && product.Name != updatedProduct.Name)
        {
            return nameValidationResponse;
        }

        product.Name = updatedProduct.Name;
        product.Price = updatedProduct.Price;
        product.Description = updatedProduct.Description;
        await Commit();

        return new ServiceResponse(true, "Product updated successfully.");
    }

    public async Task<ServiceResponse> DeleteProduct(int id)
    {
        var product = await GetCurrentScope<Product>().FindAsync(id);
        if (product == null)
        {
            return new ServiceResponse(false, "Product not found.");
        }

        GetCurrentScope<Product>().Remove(product);
        await Commit();

        return new ServiceResponse(true, "Product deleted successfully.");
    }
}