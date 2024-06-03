using Microsoft.EntityFrameworkCore;
using webapp.src.Data;
using webapp.src.Data.Models;

namespace webapp.src.Repositories;


public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product?> GetProductByIdAsync(string id);
    Task<Product> CreateProduct(Product product);
    Task<Product?> UpdateProduct(Product product);
    Task<bool> DeleteProduct(string id);
}

public class ProductRepository(LContext context) : IProductRepository
{
    private readonly LContext _context = context;

    public async Task<Product> CreateProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        int affectedRows = 0;
        // Attempt to delete the product using .Remove() with filtering
        affectedRows = await _context.Products.Where(p => p.ID == id).ExecuteDeleteAsync();

        // Return true if at least one row was affected (indicating deletion)
        return affectedRows > 0;
    }

    public async Task<Product?> GetProductByIdAsync(string id)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.ID == id);
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> UpdateProduct(Product product)
    {   
        var item = await _context.Products.FindAsync(product.ID);

        if (item != null)
        {
            // Update properties on the existing product (assuming data transfer object pattern is not used)
            item.Label = product.Label;
            item.Price = product.Price;
            item.Type = product.Type;
            // Update other properties as needed

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the updated product
            return item;
        }

        return null; //  if not found
    }
}