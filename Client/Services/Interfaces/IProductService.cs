using Torico.Shared.Models;
using Torico.Shared.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Torico.Client.Services.Interfaces;

public interface IProductService
{
    Task<ServiceResponse> AddProduct(Product model);
    Task<List<Product>> GetAllProducts(bool isFeatured);
}