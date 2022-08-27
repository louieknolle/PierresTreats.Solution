using Microsoft.AspNetCore.Mvc;

namespace SweetShop.Controllers
{
    public class HomeController : Controller

    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
