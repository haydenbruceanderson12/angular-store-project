using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreDbContext _storeDbContext;

        public ProductsController(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _storeDbContext.Products.ToListAsync());
        }

        [HttpGet("GetProductById/{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            return Ok(await _storeDbContext.Products.FindAsync(id));
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            _storeDbContext.Products.Add(product);
            await _storeDbContext.SaveChangesAsync();

            return Ok(product);
        }

        [HttpDelete("DeleteProduct/{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _storeDbContext.Products.FindAsync(id);
            
            if (product is null) return NotFound();

            _storeDbContext.Remove(product);
            await _storeDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("UpdateProduct/{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            var existingProduct = await _storeDbContext.Products.FindAsync(id);

            if (existingProduct is null || existingProduct.Id != id) return NotFound();

            _storeDbContext.Update(product);
            await _storeDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
