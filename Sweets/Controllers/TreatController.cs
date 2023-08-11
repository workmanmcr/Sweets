using Microsoft.AspNetCore.Mvc;
using Sweets.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Sweets.Controllers 
{
    [Authorize]
    public class TreatsController : Controller  
    {
        private readonly SweetsContext _db;
         private readonly UserManager<ApplicationUser> _userManager;  

        public TreatsController(UserManager<ApplicationUser> userManager,SweetsContext db) 
        {
            _userManager = userManager;
            _db = db;  
        }

        public ActionResult Index()
        {
          string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      List<Treat> userTreats = _db.Treats
                          .Where(entry => entry.User.Id == currentUser.Id)
                          .Include(treat => treat.Flavor)
                          .ToList();
      return View(treayFlavors);
        }

        public ActionResult Create()
        {
            ViewBag.MachineId = new SelectList(_db.Flavors, "FlavorId", "FlavorName");  
            return View(treat);
        }

        [HttpPost]
        public ActionResult Create(Treat treat, int FlavorId)  
          {
      if (!ModelState.IsValid)
      {
        ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
        return View(Treat);
      }
      else
      {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        treat.User = currentUser;
        _db.Treats.Add(treat);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

        public ActionResult Details(int id)
        {
            var thisTreat = _db.Treats
                              .Include(treat => treat.JoinEntities)
                              .ThenInclude(join => join.Flavor)
                              .FirstOrDefault(treat => treat.TreatId == id);  
            return View(thisTreat);
        }

        public ActionResult Edit(int id)
        {
            var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);  
            ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "FlavorName"); 
            return View(thisTreat);
        }

        [HttpPost]
        public ActionResult Edit(Treat treat)  // Replaced "Engineer" with "Treat"
        {
            _db.Entry(treat).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Details", new { id = treat.TreatId });  // Replaced "EngineerId" with "TreatId"
        }

        public ActionResult AddFlavor(int id)  // Replaced "Machine" with "Flavor"
        {
            var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);  // Replaced "Engineer" with "Treat", "EngineerId" with "TreatId"
            ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "FlavorName");  // Replaced "MachineId" with "FlavorId", "MachineName" with "FlavorName"
            return View(thisTreat);
        }

        [HttpPost]
        public ActionResult AddFlavor(Treat treat, int FlavorId)  // Replaced "Engineer" with "Treat"
        {
            if (FlavorId != 0)  // Replaced "MachineId" with "FlavorId"
            {
                if (_db.TreatFlavors.Any(join => join.FlavorId == FlavorId && join.TreatId == treat.TreatId) == false)  // Replaced "MachineId" with "FlavorId", "EngineerId" with "TreatId"
                    _db.TreatFlavors.Add(new TreatFlavor() { FlavorId = FlavorId, TreatId = treat.TreatId });  
            }
            _db.SaveChanges();
            return RedirectToAction("Details", new { id = treat.TreatId });  
        }

        public ActionResult Delete(int id)
        {
            var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);  
            return View(thisTreat);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);  
            _db.Treats.Remove(thisTreat); 
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteFlavor(int joinId)  
        {
            var joinEntry = _db.TreatFlavors.FirstOrDefault(entry => entry.TreatFlavorId == joinId);  
            _db.TreatFlavors.Remove(joinEntry);  
            _db.SaveChanges();
            return RedirectToAction("Details", new { id = joinEntry.TreatId });  
        }
    }
}
