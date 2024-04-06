using Microsoft.AspNetCore.Mvc;

namespace BooksShopWeb.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
