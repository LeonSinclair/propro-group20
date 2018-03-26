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
        
        public IActionResult Index()
        {
            var courseList = _context.Courses.ToList();

            return View(courseList);
        }

        [HttpPost("LogHours")]
        public IActionResult LogHours()
        {
            var hour = _context.Hours;
            var userList = _context.Users;
            var courseList = _context.Courses.ToList();

            return View(Tuple.Create(hour, userList, courseList));
        }

        //welcome() method should be deleted from final version of project
        [HttpPost("Welcome")]
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;
            return View();
        }
    }
}