using Microsoft.AspNetCore.Mvc;
using SweetShop.Models;
using System.Linq;

namespace SweetShop.Controllers
{
    public class HomeController : Controller

    {
        private readonly SweetShopContext _db;
        public HomeController(SweetShopContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            ViewBag.Flavors = _db.Flavors.ToList();
            return View(_db.Treats.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
