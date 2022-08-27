using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SweetShop.Models;

namespace SweetShop.Controllers;

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
