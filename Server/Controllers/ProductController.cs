using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Torico.Server;
using Torico.Server.Data;
using Torico.Shared;
using Torico.Shared.Models;
using Torico.Shared.Responses;
using Torico.Server.Repositories;
using Torico.Server.Repositories.Interfaces;
using Torico.Server.Repositories.Core;

namespace Torico.Server.Controllers;

// This syntax works too
// public class ProductController(IProduct productService) : ControllerBase
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository productRepository;

    public ProductController(Services services)
    {
        productRepository = services.ProductRepository;
    }

    // GET: api/<ProductController>
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts(bool featured)
    {
        var products = await productRepository.GetAllProducts(featured); return Ok(products);
    }

    // GET api/<ProductController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var product = await productRepository.GetProductById(id);
        if (product == null)
        {
            return NotFound("Product not found.");
        }
        return Ok(product);
    }

    // POST api/<ProductController>
    [HttpPost]
    public async Task<ActionResult<ServiceResponse>> AddProduct(Product model)
    {
        if(model is null) return BadRequest("Model is null");
        var response = await productRepository.AddProduct(model);
        return Ok(response);
    }

    // PUT api/<ProductController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult<ServiceResponse>> UpdateProduct(int id, [FromBody] Product updatedProduct)
    {
        var response = await productRepository.UpdateProduct(id, updatedProduct);
        if (!response.IsSuccessful)
        {
            return BadRequest(response.Message);
        }
        return Ok(response);
    }

    // DELETE api/<ProductController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse>> DeleteProduct(int id)
    {
        var response = await productRepository.DeleteProduct(id);
        if (!response.IsSuccessful)
        {
            return NotFound(response.Message);
        }
        return Ok(response);
    }
}
