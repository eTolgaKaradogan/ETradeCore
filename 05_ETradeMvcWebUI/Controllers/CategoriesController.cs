using Microsoft.AspNetCore.Mvc;

namespace ETradeMvcWebUI.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
