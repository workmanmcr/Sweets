using Microsoft.AspNetCore.Mvc;
using Sweets.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sweets.Controllers 
{
    public class TreatsController : Controller  
    {
        private readonly SweetsContext _db;  

        public TreatsController(SweetsContext db) 
        {
            _db = db;  
        }

        public ActionResult Index()
        {
            IEnumerable<Treat> sortedTreats = _db.Treats.OrderBy(treat => treat.Name);  
            return View(sortedTreats.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.MachineId = new SelectList(_db.Flavors, "FlavorId", "FlavorName");  
            return View();
        }

        [HttpPost]
        public ActionResult Create(Treat treat, int FlavorId)  
        {
            _db.Treats.Add(treat);  
            _db.SaveChanges();
            if (FlavorId != 0)  
            {
                _db.TreatFlavors.Add(new TreatFlavor() { FlavorId = FlavorId, TreatId = treat.TreatId });  
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var thisTreat = _db.Treats
                              .Include(treat => treat.JoinEntities)
                              .ThenInclude(join => join.Flavor)
                              .FirstOrDefault(treat => treat.TreatId == id);  // Replaced "Engineer" with "Treat", "EngineerId" with "TreatId"
            return View(thisTreat);
        }

        public ActionResult Edit(int id)
        {
            var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);  // Replaced "Engineer" with "Treat", "EngineerId" with "TreatId"
            ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "FlavorName");  // Replaced "MachineId" with "FlavorId", "MachineName" with "FlavorName"
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
