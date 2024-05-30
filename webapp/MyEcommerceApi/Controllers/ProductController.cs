namespace MyApp.Namespace;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyEcommerceApi.Data;
using MyEcommerceApi.Services;

[Route("api/[controller]")]
[ApiController]
public class ProductController(IProductService service) : ControllerBase
{
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
