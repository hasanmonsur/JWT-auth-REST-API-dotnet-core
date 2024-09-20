using DapperApi.Contacts;
using DapperApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DapperApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            var result = await _productRepository.AddProduct(product);
            if (result > 0) return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            product.Id = id;
            var result = await _productRepository.UpdateProduct(product);
            if (result > 0) return NoContent();
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productRepository.DeleteProduct(id);
            if (result > 0) return NoContent();
            return BadRequest();
        }
    }

}
