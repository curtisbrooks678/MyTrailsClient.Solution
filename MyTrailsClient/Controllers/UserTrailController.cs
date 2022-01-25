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
  public class UserTrailController : Controller
  {
    private readonly MyTrailsClientContext _db;
    public UserTrailController(MyTrailsClientContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<UserTrail> model = _db.UserTrails.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(UserTrail userTrail)
    {
      _db.UserTrails.Add(userTrail);
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
      return View(thisUserTrail);
    }

    [HttpPost]
    public ActionResult Edit(UserTrail userTrail)
    {
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
    
  }
}