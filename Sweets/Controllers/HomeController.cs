using Microsoft.AspNetCore.Mvc;
using Sweets.Models;  
using System.Collections.Generic;
using System.Linq;

namespace Sweets.Controllers 
{
    public class HomeController : Controller
    {
        private readonly SweetsContext _db;  

        public HomeController(SweetsContext db) 
        {
            _db = db; 
        }

        [HttpGet("/")]

        public ActionResult Index()
        {
            ViewBag.EmployeeDirectory = new List<Treat>(_db.Treats.OrderBy(treat => treat.Name));  
            ViewBag.MachineInventory = new List<Flavor>(_db.Flavors.OrderBy(flavor => flavor.FlavorName));  
             return View();
        }
    }
}
