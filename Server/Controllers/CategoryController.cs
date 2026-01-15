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
// public class CategoryController(ICategory categoryService) : ControllerBase
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository categoryRepository;

    public CategoryController(Services services)
    {
        categoryRepository = services.CategoryRepository;
    }

    // GET: api/<CategoryController>
    [HttpGet]
    public async Task<ActionResult<List<Category>>> GetCategories()
    {
        var categories = await categoryRepository.GetAllCategories(); return Ok(categories);
    }

    // GET api/<CategoryController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategoryById(int id)
    {
        var category = await categoryRepository.GetCategoryById(id);
        if (category == null)
        {
            return NotFound("Category not found.");
        }
        return Ok(category);
    }

    // POST api/<CategoryController>
    [HttpPost]
    public async Task<ActionResult<ServiceResponse>> AddCategory(Category model)
    {
        if(model is null) return BadRequest("Model is null");
        var response = await categoryRepository.AddCategory(model);
        return Ok(response);
    }

    // PUT api/<CategoryController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult<ServiceResponse>> UpdateCategory(int id, [FromBody] Category updatedCategory)
    {
        var response = await categoryRepository.UpdateCategory(id, updatedCategory);
        if (!response.IsSuccessful)
        {
            return BadRequest(response.Message);
        }
        return Ok(response);
    }

    // DELETE api/<CategoryController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse>> DeleteCategory(int id)
    {
        var response = await categoryRepository.DeleteCategory(id);
        if (!response.IsSuccessful)
        {
            return NotFound(response.Message);
        }
        return Ok(response);
    }
}
