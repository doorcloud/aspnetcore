namespace MyApp.Namespace;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapp.src.Data;
using webapp.src.Services;

[Route("api/[controller]")]
[ApiController]
public class ProductController(ILogger<ProductController> logger, IProductService service) : ControllerBase
{
    private readonly ILogger<ProductController> _logger = logger;
    private readonly IProductService _productService = service;

    [HttpGet("products/{id}")]
    public async Task<IActionResult> GetProduct(string id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }
}
