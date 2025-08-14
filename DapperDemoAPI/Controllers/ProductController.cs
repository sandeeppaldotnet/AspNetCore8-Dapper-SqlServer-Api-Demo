using DapperDemoAPI.Models;
using DapperDemoAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DapperDemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        // GET: api/product?useStoredProc=true
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool useStoredProc = false)
        {
            var products = await _repository.GetAllAsync(useStoredProc);
            return Ok(products);
        }

        // GET: api/product/5?useStoredProc=true
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, [FromQuery] bool useStoredProc = false)
        {
            var product = await _repository.GetByIdAsync(id, useStoredProc);
            return product == null ? NotFound() : Ok(product);
        }

        // POST: api/product?useStoredProc=true
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product, [FromQuery] bool useStoredProc = false)
        {
            var id = await _repository.CreateAsync(product, useStoredProc);
            return CreatedAtAction(nameof(GetById), new { id, useStoredProc }, product);
        }

        // PUT: api/product/5?useStoredProc=true
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product product, [FromQuery] bool useStoredProc = false)
        {
            if (id != product.Id) return BadRequest();
            var updated = await _repository.UpdateAsync(product, useStoredProc);
            return updated ? NoContent() : NotFound();
        }

        // DELETE: api/product/5?useStoredProc=true
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] bool useStoredProc = false)
        {
            var deleted = await _repository.DeleteAsync(id, useStoredProc);
            return deleted ? NoContent() : NotFound();
        }
    }
}
