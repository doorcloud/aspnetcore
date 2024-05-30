using MyEcommerceApi.Models;
using MyEcommerceApi.Repositories;

namespace MyEcommerceApi.Services;

public interface IProductService
{
    Task<Product> GetProductByIdAsync(string id);
}

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<Product> GetProductByIdAsync(string id)
    {
        return await _productRepository.GetProductByIdAsync(id);
    }

}