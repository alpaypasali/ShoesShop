using Microsoft.AspNetCore.Mvc;

namespace ShopShop.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
