using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomelo.Models;

namespace Pomelo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            return product;
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // POST: api/products
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // POST: api/products/by-ids
        [HttpPost("Get-Wit-Array-by-ids")]
        public async Task<IActionResult> GetByIds([FromBody] int[] ids)
        {
            if (ids == null || ids.Length == 0)
                return BadRequest("Ids list cannot be empty");

            var products = await _context.Products
                                         .Where(p => ids.Contains(p.Id))
                                         .ToListAsync();

            return Ok(products);
        }
        
        [HttpPost("Get-With-Enumerable-by-ids")]
        public async Task<IActionResult> GetByWithEnumerable([FromBody] List<int> ids)
        {
            if (ids == null || ids.Count == 0)
                return BadRequest("Ids list cannot be empty");

            var products = await _context.Products
                                         .Where(p => ids.Contains(p.Id))
                                         .ToListAsync();

            return Ok(products);
        }
    }

}
