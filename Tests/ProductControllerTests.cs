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

public class ProductControllerTests
{
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var appDbContext = new AppDbContext(options);
        appDbContext.Products.Add(new Product { Id = 1, Name = "Test Product", Featured = true });
        appDbContext.SaveChanges();
        _mockProductRepository = new Mock<IProductRepository>();
        var mockCategoryRepository = new Mock<ICategoryRepository>();
        var services = new Services(appDbContext, _mockProductRepository.Object, mockCategoryRepository.Object);
        _controller = new ProductController(services);
    }

    [Fact]
    public async Task GetProductById_ReturnsProduct_WhenProductExists()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Test Product" };
        _mockProductRepository.Setup(repo => repo.GetProductById(1)).ReturnsAsync(product);

        // Act
        var result = await _controller.GetProductById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(product, okResult.Value);
    }

    [Fact]
    public async Task GetProductById_ReturnsNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        _mockProductRepository.Setup(repo => repo.GetProductById(1)).ReturnsAsync((Product?)null);

        // Act
        var result = await _controller.GetProductById(1);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task AddProduct_ReturnsOk_WhenProductIsAdded()
    {
        // Arrange
        var product = new Product { Name = "New Product" };
        _mockProductRepository.Setup(repo => repo.AddProduct(product)).ReturnsAsync(new ServiceResponse(true, "Product added successfully."));

        // Act
        var result = await _controller.AddProduct(product);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.True(((ServiceResponse)okResult.Value!).IsSuccessful);
    }
}