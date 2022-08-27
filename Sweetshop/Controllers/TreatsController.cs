using Microsoft.AspNetCore.Mvc;
using SweetShop.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

//new using directives
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

//////////////////////////////////////////////////////
//////// Authorizing Create, Update and Delete of TreatsController.cs 
//////////////////////////////////////////////////////
//////// 1. Item.cs needs ApplicationUser property
//////// 2. ItemsController.cs has various updates
//////// 3. Views/Items/Details.cshtml has updates
//////// 4. Views/Items/Index.cshtml has updates
//////////////////////////////////////////////////////

namespace SweetShop.Controller
{
  public class TreatsController : Controller
  {
    private readonly SweetShopContext _db;
    private readonly UserManager<ApplicationUser> _userManager; 

    public TreatsController(UserManager<ApplicationUser> userManager, SweetShopContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    //Index Route updated to find all DB Treats
    public ActionResult Index()
    {
      List<Treat> userTreats = _db.Treats.ToList();
      return View(userTreats);
    }

    //Create Route updated to Add Authorization
    [Authorize] 
    public ActionResult Create()
    {
      ViewBag.FlavorId = new SelectList(_db.Categories, "FlavorId", "Name");
      return View();
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(Treat treat, int FlavorId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      treat.User = currentUser;
      _db.Treats.Add(treat);
      _db.SaveChanges();
      if (FlavorId != 0)
      {
        _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    // In the Details route we need to find the user associated with the item so that in the view, we can show the edit, delete or add Flavor links if the item "belongs" to that user. Line 93 involves checking if the userId is null: if it is null then IsCurrentUser is set to false, if is not null, then the program evaluates whether userId is equal to thisItem.User.Id and returns true if so, false if not so.
    // Line 93 can be refactored into an if statement like so:
    // if (userId != null) 
    // {
    //   if (userId == thisItem.User.Id) 
    //   {
    //     ViewBag.IsCurrentUser = true;
    //   }
    //   else
    //   {
    //     ViewBag.IsCurrentUser = false;
    //   }
    // }
    // else 
    // {
    //   ViewBag.IsCurrentUser = false;
    // }
    // Look at the Details view and how IsCurrentUser is used to help comprehend what is happening in the conditional using the ternary operator 
    public ActionResult Details(int id)
    {
      var thisTreat = _db.Treats
          .Include(treat => treat.JoinEntities)
          .ThenInclude(join => join.Flavor)
          .Include(treat => treat.User)
          .FirstOrDefault(treat => treat.TreatId == id);
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ViewBag.IsCurrentUser = userId != null ? userId == thisTreat.User.Id : false;
      return View(thisTreat);
    }

    // Edit Route is updated to find the user and the item that matches the user id, then is routed based on that result. 
    [Authorize]
    public async Task<ActionResult> Edit(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var thisTreat = _db.Treats
          .Where(entry => entry.User.Id == currentUser.Id)
          .FirstOrDefault(treat => treat.TreatId == id);
      if (thisTreat == null)
      {
        return RedirectToAction("Details", new {id = id});
      }
      ViewBag.FlavorId = new SelectList(_db.Categories, "FlavorId", "Name"); 
      return View(thisTreat);
    }


    [HttpPost]
    public ActionResult Edit(Treat treat, int FlavorId)
    {
      if (FlavorId != 0)
      {
        _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId });
      }
      _db.Entry(treat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    // AddFlavor is updated to find the user and the item that matches the user id, then is routed based on that result. 
    [Authorize]
    public async Task<ActionResult> AddFlavor(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      Treat thisTreat = _db.Treats
          .Where(entry => entry.User.Id == currentUser.Id)
          .FirstOrDefault(treat => treat.TreatId == id);
      if (thisTreat == null)
      {
        return RedirectToAction("Details", new {id = id});
      }
      ViewBag.FlavorId = new SelectList(_db.Categories, "FlavorId", "Name");
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult AddFlavor(Treat treat, int FlavorId)
    {
      if (FlavorId != 0)
      {
        _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    // Delete route is updated to find the user and the item that matches the user id, then is routed based on that result. 
    [Authorize]
    public async Task<ActionResult> Delete(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);

      Treat thisTreat = _db.Treats
          .Where(entry => entry.User.Id == currentUser.Id)
          .FirstOrDefault(treat => treat.TreatId == id);
      if (thisTreat == null)
      {
        return RedirectToAction("Details", new {id = id});
      }
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
      var joinEntry = _db.FlavorTreat.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
      _db.FlavorTreat.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}