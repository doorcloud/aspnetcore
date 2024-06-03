namespace webapp.src.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapp.src.Data;
using webapp.src.Data.DTO;
using webapp.src.Data.Models;
using webapp.src.Services;

[Route("api/v1/products")]
[ApiController]
public class ProductController(ILogger<ProductController> logger, IProductService service) : ControllerBase
{
    private readonly ILogger<ProductController> _logger = logger;
    private readonly IProductService _productService = service;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(string id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateProduct([FromBody] ProductDTO product)
    {  
        try
        {
            var newProduct = new Product{ID=Guid.NewGuid().ToString(),Label=product.Label,Type=product.Type,Stock=product.Stock,Price=product.Price};
            var createdProduct = await _productService.CreateProduct(newProduct);
            return CreatedAtRoute("GetProduct", new { id = createdProduct.ID }, createdProduct);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = "Invalid product data", details = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error creating product", details = ex.Message });
        }
    }
}
