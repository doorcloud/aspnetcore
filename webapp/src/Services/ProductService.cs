using webapp.src.Data.DTO;
using webapp.src.Data.Models;
using webapp.src.Repositories;

namespace webapp.src.Services;

public interface IProductService
{
    Task<Product?> GetProductByIdAsync(string id);
    Task<Product> CreateProduct(Product product);
}

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<Product?> GetProductByIdAsync(string id)
    {
        return await _productRepository.GetProductByIdAsync(id);
    }


    public async Task<Product> CreateProduct(Product product) {
        if (product == null) 
        {
            throw new ArgumentException("Invalid product data provided");
        }

        return await _productRepository.CreateProduct(product) ?? throw new Exception("Error creating product");
    }


}