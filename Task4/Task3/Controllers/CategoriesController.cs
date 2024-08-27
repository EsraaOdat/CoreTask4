using Microsoft.AspNetCore.Mvc;
using Task3.Models;

namespace Task3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly MyDbContext _db;

        public CategoriesController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("AllCategories")]
        public IActionResult GetAllCategories()
        {
            var data = _db.Categories.ToList();
            return Ok(data);
        }

        [HttpGet]
        [Route("Category/{id:int:min(3)}")]
        public IActionResult GetCategoryById(int id)
        {
            var data = _db.Categories.Find(id);
            if (data == null)
            {
                return NotFound(new { message = "Category not found" });
            }
            return Ok(data);
        }

        [HttpGet]
        [Route("Category/{name}")]
        public IActionResult GetCategoryByName(string name)
        {
            var data = _db.Categories.FirstOrDefault(c => c.CategoryName == name);
            if (data == null)
            {
                return NotFound(new { message = "Category not found" });
            }
            return Ok(data);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteCategory(int id)
        {
            var data = _db.Categories.Find(id);
            if (data == null)
            {
                return NotFound(new { message = "Category not found" });
            }
            _db.Products.RemoveRange(data.Products);
            _db.Categories.Remove(data);
            _db.SaveChanges();
            return Ok(new { message = "Category deleted", category = data });
        }
    }
}
