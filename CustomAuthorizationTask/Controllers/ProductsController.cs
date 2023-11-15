using CustomAuthorizationTask.Data;
using CustomAuthorizationTask.Dto;
using CustomAuthorizationTask.Models;
using CustomAuthorizationTask.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomAuthorizationTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetProducts")]
        [Authorize(Roles = "Admin",Policy = Policies.RequireProductsView)]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpPost("CreateProduct")]
        [Authorize(Roles = "Admin",Policy = Policies.RequireProductsCreate)]
        public async Task<IActionResult> CreateProduct(ProductDto productDto)
        {
            var product = new Products
            {
                ProductName = productDto.ProductName
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
        }

        //update
        [HttpPut("Update/{id}")]
        [Authorize(Policy = Policies.RequireProductsUpdate)]
        public async Task<IActionResult> PutProduct(int id, Products product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        //delete
        [HttpDelete("ProductDelete/{id}")]
        [Authorize(Policy = Policies.RequireProductsDelete)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}