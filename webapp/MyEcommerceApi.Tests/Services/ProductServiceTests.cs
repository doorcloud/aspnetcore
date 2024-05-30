namespace MyEcommerceApi.Tests.Services;
using MyEcommerceApi.Services;
using MyEcommerceApi.Repositories;

public class ProductServiceTests
{
    private readonly IProductRepository _mockProductRepository;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _mockProductRepository = Mock.Of<IProductRepository>();
        _productService = new ProductService(_mockProductRepository);
    }

    [Fact]
    public void GetProductById_ReturnsProduct_WhenExists()
    {
        // Arrange
        var product = new Product { Id = "fffffffffffff" Name = "Test Product" };

        Mock.Get(_mockProductRepository).Setup(repo => repo.GetProductByIdAsync(productId))
            .Returns(Task.FromResult(product));

        // Act
        var retrievedProduct = _productService.GetProductByIdAsync(productId).Result;

        // Assert
        Assert.NotNull(retrievedProduct);
        Assert.Equal(product.Id, retrievedProduct.Id);
        Assert.Equal(product.Name, retrievedProduct.Name);
    }

    [Fact]
    public void GetProductById_ReturnsNull_WhenProductNotFound()
    {
        // Arrange
        string nonExistentId = "eeeeeeeeeeeee";

        Mock.Get(_mockProductRepository).Setup(repo => repo.GetProductByIdAsync(nonExistentId))
            .Returns(Task.FromResult<Product>(null));

        // Act
        var retrievedProduct = _productService.GetProductByIdAsync(nonExistentId).Result;

        // Assert
        Assert.Null(retrievedProduct);
    }
}