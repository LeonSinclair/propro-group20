using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AllocationApp.Models;
using AllocationApp.Data;

namespace AllocationApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AllocationContext _context;

        public HomeController(AllocationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Lecturer()
        {
            ViewData["Message"] = "Lecturer View.";
            return View();
        }

        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            var results = _context.Subordinates.ToList();
            return View(results);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
