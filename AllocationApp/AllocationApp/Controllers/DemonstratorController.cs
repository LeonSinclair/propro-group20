using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using AllocationApp.Data;

namespace AllocationApp.Controllers
{
    public class DemonstratorController : Controller
    {
        private readonly AllocationContext _context;
        
        public DemonstratorController(AllocationContext context)
        {
            _context = context;
        }

        // 
        // GET: /Demonstrator/
        public IActionResult Index()
        {
            ViewData["Message"] = _context;
            var courses = _context.Courses.ToList();
            return View(courses);
        }

        // GET: /Demonstrator/Welcome/ 
        // Requires using System.Text.Encodings.Web
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;
            return View();
        }
    }
}