using Microsoft.AspNetCore.Mvc;
using MyTrailsClient.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Dynamic;

namespace MyTrailsClient.Controllers
{
  [Authorize]
  public class UserTrailsController : Controller
  {
    private readonly MyTrailsClientContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public UserTrailsController(UserManager<ApplicationUser> userManager, MyTrailsClientContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userUserTrails = _db.UserTrails
        .Where(entry => entry.User.Id == currentUser.Id)
        .ToList();
      ViewData["SixtyTrails"]= _db.UserTrails.ToList();  
      return View(userUserTrails);
    } 

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(UserTrail userTrail, int VisitEntryId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      userTrail.User = currentUser;
      _db.UserTrails.Add(userTrail);
      _db.SaveChanges();
      if (VisitEntryId != 0)
      {
        _db.UserTrailVisitEntry.Add(new UserTrailVisitEntry() { VisitEntryId = VisitEntryId, UserTrailId = userTrail.UserTrailId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
        var thisUserTrail = _db.UserTrails
        .Include(userTrail => userTrail.JoinEntities)
        .ThenInclude(join => join.VisitEntry)
        .FirstOrDefault(userTrail => userTrail.UserTrailId == id);
        return View(thisUserTrail);
    }

    public ActionResult Edit(int id)
    {
      var thisUserTrail = _db.UserTrails.FirstOrDefault(userTrail => userTrail.UserTrailId == id);
      ViewBag.UserTrailId = new SelectList(_db.UserTrails, "UserTrailId", "Name");
      return View(thisUserTrail);
    }

    [HttpPost]
    public ActionResult Edit(UserTrail userTrail, int VisitEntryId)
    {
      if (VisitEntryId != 0)
      {
        _db.UserTrailVisitEntry.Add(new UserTrailVisitEntry() { VisitEntryId = VisitEntryId, UserTrailId = userTrail.UserTrailId });
      }
      _db.Entry(userTrail).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisUserTrail = _db.UserTrails.FirstOrDefault(userTrail => userTrail.UserTrailId == id);
      return View(thisUserTrail);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisUserTrail = _db.UserTrails.FirstOrDefault(userTrail => userTrail.UserTrailId == id);
      _db.UserTrails.Remove(thisUserTrail);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteVisitEntry(int joinId)
    {
      var joinEntry = _db.UserTrailVisitEntry.FirstOrDefault(entry => entry.UserTrailId == joinId);
      _db.UserTrailVisitEntry.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddVisitEntry(int id)
    {
      var thisUserTrail = _db.UserTrails.FirstOrDefault(userTrail => userTrail.UserTrailId == id);
      ViewBag.VisitEntryId = new SelectList(_db.VisitEntries, "VisitEntryId", "VisitDate");
      return View(thisUserTrail);
    }

    [HttpPost]
    public ActionResult AddVisitEntry(UserTrail userTrail, int VisitEntryId)
    {
      if (VisitEntryId != 0)
      {
        _db.UserTrailVisitEntry.Add(new UserTrailVisitEntry() { VisitEntryId = VisitEntryId, UserTrailId = userTrail.UserTrailId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddVisitEntryError(int id)
    {
      var thisUserTrail = _db.UserTrails.FirstOrDefault(userTrail => userTrail.UserTrailId == id);
      return View(thisUserTrail);
    }

    public ActionResult AddExistingTrail(int id, int UserTrailId)
    {
      var allUserTrails = _db.UserTrails.FirstOrDefault(userTrail => userTrail.UserTrailId == id);
      ViewBag.UserTrailId = new SelectList(_db.UserTrails, "UserTrailId", "Name");
      return View(allUserTrails);
    }

    [HttpPost]
    public async Task<ActionResult> AddExistingTrail(UserTrail userTrail)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userTrailTestList = _db.UserTrails
        .Where(entry => entry.UserTrailId == 1)
        .ToList();
      var userTrailTest = userTrailTestList[0];
      var userId1 = userTrailTest.User.Id;
      userId1 = currentUser.Id;
      return RedirectToAction("Index");
    }
    
  }
}