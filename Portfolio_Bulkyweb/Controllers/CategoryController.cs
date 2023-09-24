using Microsoft.AspNetCore.Mvc;

namespace Portfolio_Bulkyweb.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
