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

namespace MyTrailsClient.Controllers 
{
  [Authorize]
  public class VisitEntriesController : Controller
  {
    private readonly MyTrailsClientContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public VisitEntriesController(UserManager<ApplicationUser> userManager, MyTrailsClientContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userVisitEntries = _db.VisitEntries
        .Where(entry => entry.User.Id == currentUser.Id)
        .ToList();
      return View(userVisitEntries);
    } 

    public async Task<ActionResult> Create()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userUserTrails = _db.UserTrails
        .Where(entry => entry.User.Id == currentUser.Id)
        .ToList();
      ViewBag.UserTrailId = new SelectList(userUserTrails, "UserTrailId", "Name");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(VisitEntry visitEntry, int UserTrailId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      visitEntry.User = currentUser;
      _db.VisitEntries.Add(visitEntry);
      _db.SaveChanges();
      if (UserTrailId != 0)
      {
        _db.UserTrailVisitEntry.Add(new UserTrailVisitEntry() { UserTrailId = UserTrailId, VisitEntryId = visitEntry.VisitEntryId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
        var thisVisitEntry = _db.VisitEntries
        .Include(visitEntry => visitEntry.JoinEntities)
        .ThenInclude(join => join.UserTrail)
        .FirstOrDefault(visitEntry => visitEntry.VisitEntryId == id);
        return View(thisVisitEntry);
    }

    public ActionResult Edit(int id)
    {
      var thisVisitEntry = _db.VisitEntries.FirstOrDefault(visitEntry => visitEntry.VisitEntryId == id);
      ViewBag.UserTrailId = new SelectList(_db.UserTrails, "UserTrailId", "Name");
      return View(thisVisitEntry);
    }

    [HttpPost]
    public ActionResult Edit(VisitEntry visitEntry, int UserTrailId)
    {
      if (UserTrailId != 0)
      {
        _db.UserTrailVisitEntry.Add(new UserTrailVisitEntry() { UserTrailId = UserTrailId, VisitEntryId = visitEntry.VisitEntryId });
      }
      _db.Entry(visitEntry).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisVisitEntry = _db.VisitEntries.FirstOrDefault(visitEntry => visitEntry.VisitEntryId == id);
      return View(thisVisitEntry);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisVisitEntry = _db.VisitEntries.FirstOrDefault(visitEntry => visitEntry.VisitEntryId == id);
      _db.VisitEntries.Remove(thisVisitEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteUserTrail(int joinId)
    {
      var joinEntry = _db.UserTrailVisitEntry.FirstOrDefault(entry => entry.UserTrailVisitEntryId == joinId);
      _db.UserTrailVisitEntry.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public async Task<ActionResult> AddUserTrail(int id)
    {
      var thisVisitEntry = _db.VisitEntries.FirstOrDefault(visitEntry => visitEntry.VisitEntryId == id);
      
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userUserTrails = _db.UserTrails
        .Where(entry => entry.User.Id == currentUser.Id)
        .ToList();
      
      ViewBag.UserTrailId = new SelectList(userUserTrails, "UserTrailId", "Name");
      return View(thisVisitEntry);
    }

    [HttpPost]
    public ActionResult AddUserTrail(VisitEntry visitEntry, int UserTrailId)
    {
      if (UserTrailId != 0)
      {
        _db.UserTrailVisitEntry.Add(new UserTrailVisitEntry() { UserTrailId = UserTrailId, VisitEntryId = visitEntry.VisitEntryId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddUserTrailError(int id)
    {
      var thisVisitEntry = _db.VisitEntries.FirstOrDefault(visitEntry => visitEntry.VisitEntryId == id);
      return View(thisVisitEntry);
    }
    
  }
}