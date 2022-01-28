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
using System.Dynamic;

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

		public ActionResult Details(int id)
		{ 
			var thisTrail = ApiTrail.GetDetails(id);
			return View(thisTrail);
		}

		public IActionResult ExistingTrails()
		{
			var allTrails = ApiTrail.GetApiTrails();
      return View(allTrails);
		}

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
