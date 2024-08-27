using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Task3.Models;

namespace Task3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _db;

        public ProductsController(MyDbContext db)
        {
            _db = db;
        }

        // Get all products
        [HttpGet("AllProducts")]
        public IActionResult GetAllProducts()
        {
            var data = _db.Products.ToList();
            return Ok(data);
        }

        // Get products by category ID
        [HttpGet("ProductsByCategoryId/{categoryId}")]
        public IActionResult GetProductsByCategoryId(int categoryId)
        {
            var products = _db.Products.Where(x => x.CategoryId == categoryId).ToList();
            return Ok(products);
        }


        // Get a single product by ID
        [HttpGet("Product/{id}")]
        public IActionResult GetProductById(int id)
        {
            var data = _db.Products.FirstOrDefault(p => p.ProductId == id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // Get product by ID with a name filter
        [HttpGet("ProductByName/{id:int:max(10)}")]
        public IActionResult GetProductByIdAndName(int id, [FromQuery] string name)
        {
            var data = _db.Products.FirstOrDefault(c => c.ProductId == id && c.ProductName == name);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // Delete a product by ID
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var data = _db.Products.Find(id);
            if (data == null)
            {
                return NotFound();
            }
            _db.Products.Remove(data);
            _db.SaveChanges();
            return Ok(data);
        }
    }
}
