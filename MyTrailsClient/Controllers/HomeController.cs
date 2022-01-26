using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyTrailsClient.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyTrailsClient.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		[HttpGet("/")]

		public IActionResult Index()
		{
			ViewBag.TrailId = new SelectList(ApiTrail.GetApiTrails(), "TrailId", "Name");
			return View();
		}

		public IActionResult Details(int id)
		{
			// ViewBag.TrailId = new SelectList(ApiTrail.GetApiTrails(), "TrailId", "Name");
			
			var thisTrail = ApiTrail.GetDetails(id);
			// var id = apiTrail.TrailId;
			return View(thisTrail) ;
		}
		// RedirectToAction("Details/@{apiTrail.TrailId}")

		// "/Trails/Details/@(trail.TrailId)"

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		
	}
}
