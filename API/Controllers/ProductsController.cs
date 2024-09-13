using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications.Products;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IGenericRepository<Product> _repository;

        public ProductsController(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts([FromQuery]ProductSpecificationParameters parameters)
        {
            var specification = new ProductSpecification(parameters);

            var products = await _repository.GetAllWithSpecificationAsync(specification);
            var count = await _repository.CountAsync(specification);

            var paginatedResult = new Pagination<Product>(parameters.PageIndex, parameters.PageSize, count, products);

            return Ok(paginatedResult);
        }

        [HttpGet("GetProductById/{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _repository.GetEntityByIdAsync(id);

            return Ok(product);
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            _repository.Create(product);

            var changeWasSuccessful = await _repository.SaveChangesAsync();

            if (changeWasSuccessful is false) return BadRequest();

            return Ok(product);
        }

        [HttpDelete("DeleteProduct/{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _repository.GetEntityByIdAsync(id);
            
            if (product is null) return NotFound();

            _repository.Delete(product);

            var changeWasSuccessful = await _repository.SaveChangesAsync();

            if (changeWasSuccessful is false) return BadRequest();

            return NoContent();
        }

        [HttpPut("UpdateProduct/{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            var productExists = _repository.Exists(id);

            if (productExists is false) return BadRequest();

            _repository.Update(product);

            var changeWasSuccessful = await _repository.SaveChangesAsync();

            if (changeWasSuccessful is false) return BadRequest();

            return NoContent();
        }

        [HttpGet("GetProductBrands")]
        public async Task<IActionResult> GetProductBrands()
        {
            var specification = new BrandListSpecification();

            var brands = await _repository.GetAllWithSpecificationAsync(specification);

            return Ok(brands);
        }

        [HttpGet("GetProductTypes")]
        public async Task<IActionResult> GetProductTypes()
        {
            var specification = new TypeListSpecification();

            var types = await _repository.GetAllWithSpecificationAsync(specification);

            return Ok(types);
        }
    }
}
