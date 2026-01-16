using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechWorldService.Data;
using TechWorldService.Models;

namespace TechWorldService.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts() =>
            await context.Products.ToListAsync();

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            product.Id = 0;
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpPost("order")]
        public async Task<IActionResult> PostOrder([FromBody] OrderRequest req)
        {
            var product = await context.Products
                .FirstOrDefaultAsync(p => p.ProductId == req.ProductId);

            if (product == null)
            {
                return NotFound("Product not found in TechWorld.");
            }

            if (product.AvailableQuantity < req.Quantity)
            {
                return BadRequest("TechWorld does not have enough stock.");
            }

            product.AvailableQuantity -= req.Quantity;
            await context.SaveChangesAsync();

            return Ok(new { Message = "Order processed successfully by TechWorld!" });
        }
    }
}