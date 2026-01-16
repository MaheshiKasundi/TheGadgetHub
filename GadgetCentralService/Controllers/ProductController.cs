using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GadgetCentralService.Data;
using GadgetCentralService.Models;

namespace GadgetCentralService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // 1. GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // 2. POST: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            product.Id = 0;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        // 3. POST: api/Product/order
          [HttpPost("order")]
        public async Task<IActionResult> PostOrder([FromBody] OrderRequest req)
        {
            if (req == null || string.IsNullOrEmpty(req.ProductId))
            {
                return BadRequest("Invalid order request.");
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == req.ProductId);

            if (product == null)
            {
                return NotFound($"Product {req.ProductId} not found in GadgetCentral.");
            }

            if (product.AvailableQuantity < req.Quantity)
            {
                return BadRequest("Insufficient stock available.");
            }

            try
            {

                product.AvailableQuantity -= req.Quantity;
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Order processed successfully!" });
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Database update failed: " + ex.Message);
            }
        }
    }
}