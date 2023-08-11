using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Sweets.Models;

namespace Sweets.Controllers;

public class HomeController : Controller
{
    
    public IActionResult Index()
    {
        return View();
    }

}
