using Microsoft.AspNetCore.Mvc;
using Sweets.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sweets.Controllers  
{
    public class FlavorsController : Controller  
    {
        private readonly SweetsContext _db;  

        public FlavorsController(SweetsContext db)  
        {
            _db = db;  
        }

        public ActionResult Index()
        {
            IEnumerable<Flavor> sortedFlavors = _db.Flavors.OrderBy(flavor => flavor.FlavorName); 
            return View(sortedFlavors.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");  
            return View();
        }

        [HttpPost]
        public ActionResult Create(Flavor flavor, int TreatId)  
        {
            _db.Flavors.Add(flavor);  
            _db.SaveChanges();
            if (TreatId != 0) 
            {
                _db.TreatFlavors.Add(new TreatFlavor() { TreatId = TreatId, FlavorId = flavor.FlavorId });   
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var thisFlavor = _db.Flavors
                .Include(flavor => flavor.JoinEntities)
                .ThenInclude(join => join.Treat)  
                .FirstOrDefault(flavor => flavor.FlavorId == id);  
            
            return View(thisFlavor);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");  
            Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);  
            return View(thisFlavor);
        }

        [HttpPost]
        public ActionResult Edit(Flavor flavor)  
        {
            _db.Entry(flavor).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Details", new { id = flavor.FlavorId });  
        }

        public ActionResult AddTreat(int id)
        {
            var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id); 
            ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name"); 
            return View(thisFlavor);
        }

        [HttpPost]
        public ActionResult AddTreat(Flavor flavor, int TreatId) 
        {
            if (TreatId != 0)  
            {
                _db.TreatFlavors.Add(new TreatFlavor() { TreatId = TreatId, FlavorId = flavor.FlavorId });  
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);  
            return View(thisFlavor);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);  
            _db.Flavors.Remove(thisFlavor); 
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteTreat(int joinId)
        {
            var joinEntry = _db.TreatFlavors.FirstOrDefault(entry => entry.TreatFlavorId == joinId);  
            _db.TreatFlavors.Remove(joinEntry);  
            _db.SaveChanges();
            return RedirectToAction("Details", new { id = joinEntry.FlavorId });  
        }
    }
}
