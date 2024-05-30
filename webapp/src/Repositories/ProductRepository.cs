using webapp.src.Data;
using webapp.src.Data.Models;

namespace webapp.src.Repositories;


public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product> GetProductByIdAsync(string id);
    Task<Product> CreateProduct(Product product);
    Task UpdateProduct(Product product);
    Task DeleteProduct(string id);
}

public class ProductRepository(EcommerceContext context) : IProductRepository
{
    private readonly EcommerceContext _context = context;

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(string id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<Product> CreateProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
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
        catch 
        {
            // Handle concurrency exception (e.g., reload data or throw exception)
            throw; // You can customize the exception handling here
        }

        return product;
    }

    public async Task DeleteProduct(string id)
    {
        var product = await GetProductByIdAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
           return await _context.SaveChangesAsync();
        }
        return null;
    }
}