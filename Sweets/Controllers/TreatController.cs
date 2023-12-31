using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Sweets.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
namespace Seets.Controllers
{
  [Authorize]
  public class TreatsController : Controller
  {
    private readonly SweetsContext _db;
    private readonly UserManager<ApplicationUser> _userManager; 

    public TreatsController(UserManager<ApplicationUser> userManager, SweetsContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
        var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var currentUser = await _userManager.FindByIdAsync(userId);
        var userTreats = _db.Treats.Where(entry => entry.User.Id == currentUser.Id).ToList();
        return View(userTreats);
    }

    public ActionResult Create()
    {
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Treat treat, int FlavorId, string Name)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      treat.User = currentUser;
      _db.Treats.Add(treat);
      _db.SaveChanges();
      if (FlavorId != 0)
      {
        _db.TreatFlavor.Add(new TreatFlavor() { FlavorId = FlavorId, TreatId = treat.TreatId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisTreat = _db.Treats
          .Include(treat => treat.JoinEntities)
          .ThenInclude(join => join.Flavor)
          .FirstOrDefault(treat => treat.TreatId == id);
         
          if (thisTreat == null)
    {
        return NotFound();
    }
      return View(thisTreat);
    }
    
    public ActionResult Edit(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult Edit(Treat treat, int FlavorId)
    {
      if (FlavorId != 0)
      {
        _db.TreatFlavor.Add(new TreatFlavor() { FlavorId = FlavorId, TreatId = treat.TreatId });
      }
      _db.Entry(treat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
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

    public ActionResult AddFlavor(int id)
   {
    var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
    
    if (thisTreat == null)
    {
        return NotFound(); // Return a 404 Not Found response
    }

    // Populate the SelectList with Flavor data
    var flavors = _db.Flavors.ToList();
    ViewBag.FlavorId = new SelectList(flavors, "FlavorId", "FlavorName");

    return View(thisTreat);
}
    [HttpPost]
    public ActionResult AddFlavor(Treat treat, int FlavorId)
    {
      if (FlavorId !=0)
      {
        _db.TreatFlavor.Add(new TreatFlavor() { FlavorId = FlavorId, TreatId = treat.TreatId }); 
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteFlavor(int joinId)
    {
      var joinEntry = _db.TreatFlavor.FirstOrDefault(entry => entry.TreatFlavorId == joinId);
      _db.TreatFlavor.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
   
  }
}