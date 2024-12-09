using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Models;
using ProductsAPI.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _repository;

        public ProductController(ProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            return Ok(_repository.GetAllProducts());
        }

        [HttpGet("{type}")]
        public ActionResult<Product> GetProductByType(string type)
        {
            var product = _repository.GetProductByType(type);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = _repository.GetProductById(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }




        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _repository.AddProduct(product);
            return CreatedAtAction(nameof(GetProductByType), new { id = product.Id }, product);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest("Product ID mismatch.");

            // Check if the product exists
            var existingProduct = _repository.GetProductById(id);
            if (existingProduct == null)
                return NotFound($"Product with ID {id} not found.");

            // Perform the update
            _repository.UpdateProduct(product);

            return NoContent(); // 204 No Content on successful update
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _repository.GetProductById(id);
            if (product == null)
                return NotFound();

            _repository.DeleteProduct(id);
            return NoContent();
        }

    }
}




