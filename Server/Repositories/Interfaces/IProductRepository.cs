using Microsoft.EntityFrameworkCore;
using Torico.Server.Data;
using Torico.Shared.Models;
using Torico.Shared.Responses;

namespace Torico.Server.Repositories.Interfaces;

public interface IProductRepository
{
    Task<ServiceResponse> AddProduct(Product model);
    Task<List<Product>> GetAllProducts(bool featured);
    Task<Product?> GetProductById(int id);
    Task<ServiceResponse> UpdateProduct(int id, Product updatedProduct);
    Task<ServiceResponse> DeleteProduct(int id);
}
