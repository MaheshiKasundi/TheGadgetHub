using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectroComService.Data;
using ElectroComService.Models;

namespace ElectroComService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts() => await context.Products.ToListAsync();

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            product.Id = 0; // Let SQL handle ID
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpPost("order")]
        public async Task<IActionResult> PostOrder([FromBody] OrderRequest req)
        {
            var product = await context.Products
                .FirstOrDefaultAsync(p => p.ProductId == req.ProductId);

            if (product == null) return NotFound("Product not found.");
            if (product.AvailableQuantity < req.Quantity) return BadRequest("No stock.");

            product.AvailableQuantity -= req.Quantity; // Stock Reduction
            await context.SaveChangesAsync();

            return Ok(new { Message = "Order processed successfully!" });
        }
    }
}