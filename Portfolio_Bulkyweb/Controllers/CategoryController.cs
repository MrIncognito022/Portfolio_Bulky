using Microsoft.AspNetCore.Mvc;
using Portfolio_Bulkyweb.Data;
using Portfolio_Bulkyweb.Models;

namespace Portfolio_Bulkyweb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
         
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
    }
}
