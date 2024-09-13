using Core.Entities;
using Core.Interfaces;
using Core.Specifications.Products;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController(IGenericRepository<Product> repository) : BaseController
    {
        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts([FromQuery]ProductSpecificationParameters parameters)
        {
            var specification = new ProductSpecification(parameters);

            return await CreatePagedResult(repository, specification, parameters.PageSize, parameters.PageIndex);
        }

        [HttpGet("GetProductById/{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await repository.GetEntityByIdAsync(id);

            return Ok(product);
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            repository.Create(product);

            var changeWasSuccessful = await repository.SaveChangesAsync();

            if (changeWasSuccessful is false) return BadRequest();

            return Ok(product);
        }

        [HttpDelete("DeleteProduct/{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await repository.GetEntityByIdAsync(id);
            
            if (product is null) return NotFound();

            repository.Delete(product);

            var changeWasSuccessful = await repository.SaveChangesAsync();

            if (changeWasSuccessful is false) return BadRequest();

            return NoContent();
        }

        [HttpPut("UpdateProduct/{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            var productExists = repository.Exists(id);

            if (productExists is false) return BadRequest();

            repository.Update(product);

            var changeWasSuccessful = await repository.SaveChangesAsync();

            if (changeWasSuccessful is false) return BadRequest();

            return NoContent();
        }

        [HttpGet("GetProductBrands")]
        public async Task<IActionResult> GetProductBrands()
        {
            var specification = new BrandListSpecification();

            var brands = await repository.GetAllWithSpecificationAsync(specification);

            return Ok(brands);
        }

        [HttpGet("GetProductTypes")]
        public async Task<IActionResult> GetProductTypes()
        {
            var specification = new TypeListSpecification();

            var types = await repository.GetAllWithSpecificationAsync(specification);

            return Ok(types);
        }
    }
}
