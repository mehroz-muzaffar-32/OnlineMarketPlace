using Microsoft.AspNetCore.Mvc;
using Moq;
using Torico.Server.Controllers;
using Torico.Server.Repositories.Interfaces;
using Torico.Shared.Models;
using Torico.Shared.Responses;
using Xunit;
using Torico.Server.Repositories.Core;
using Torico.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Torico.Server.Tests.Controllers;

public class CategoryControllerTests
{
    private readonly Mock<ICategoryRepository> _mockCategoryRepository;
    private readonly CategoryController _controller;

    public CategoryControllerTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var appDbContext = new AppDbContext(options);
        appDbContext.Categories.Add(new Category { Id = 1, Name = "Test Category" });
        appDbContext.SaveChanges();
        _mockCategoryRepository = new Mock<ICategoryRepository>();
        var mockProductRepository = new Mock<IProductRepository>();
        var services = new Services(appDbContext, mockProductRepository.Object, _mockCategoryRepository.Object);
        _controller = new CategoryController(services);
    }

    [Fact]
    public async Task GetCategoryById_ReturnsCategory_WhenCategoryExists()
    {
        // Arrange
        var category = new Category { Id = 1, Name = "Test Category" };
        _mockCategoryRepository.Setup(repo => repo.GetCategoryById(1)).ReturnsAsync(category);

        // Act
        var result = await _controller.GetCategoryById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(category, okResult.Value);
    }

    [Fact]
    public async Task GetCategoryById_ReturnsNotFound_WhenCategoryDoesNotExist()
    {
        // Arrange
        _mockCategoryRepository.Setup(repo => repo.GetCategoryById(1)).ReturnsAsync((Category?)null);

        // Act
        var result = await _controller.GetCategoryById(1);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task AddCategory_ReturnsOk_WhenCategoryIsAdded()
    {
        // Arrange
        var category = new Category { Name = "New Category" };
        _mockCategoryRepository.Setup(repo => repo.AddCategory(category)).ReturnsAsync(new ServiceResponse(true, "Category added successfully."));

        // Act
        var result = await _controller.AddCategory(category);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.True(((ServiceResponse)okResult.Value!).IsSuccessful);
    }
}