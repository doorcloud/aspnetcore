using MyEcommerceApi.Data;
using MyEcommerceApi.Models;

namespace MyEcommerceApi.Repositories;


public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(string id);
}

public class ProductRepository(EcommerceContext context) : IProductRepository
{
    private readonly EcommerceContext _context = context;

    public async Task<Product> GetProductByIdAsync(string id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<Product> UpdateProductAsync(string productId, Product productToUpdate)
    {
        var product = await _context.Products.FindAsync(productId);

        if (product == null)
        {
            return null; // Product not found
        }

        product.Label= productToUpdate.Label;

        try
        {
            _context.Entry(product).Property(p => p.RowVersion).IsModified = true; // Mark RowVersion as modified
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // Handle concurrency exception (e.g., reload data or throw exception)
            throw; // You can customize the exception handling here
        }

        return product;
    }

    
}