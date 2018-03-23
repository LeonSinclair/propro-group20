using AllocationApp.Data;
using AllocationApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllocationApp.Controllers
{
    public class LecturerController : Controller
    {
        private readonly AllocationContext _context;


        public LecturerController(AllocationContext context)
        {
            _context = context;
            
        }

        public IActionResult Index()
        {
            var tup = Tuple.Create(_context.Courses.ToList(), _context.Users.ToList());
            return View(tup);
        }

        public IActionResult LecturerDashboard()
        {
            var tup = Tuple.Create(_context.Courses.ToList(), _context.Users.ToList());
            return View(tup);
        }


        //TODO this probably isn't accesible during normal usage, but good to have in case something odd happens 
        [HttpGet("ModuleDemonstrators")]
        public IActionResult ModuleDemonstrators()
        {
            var module = _context.Courses.First();
            var users = _context.Users.ToList();
            return View("ModuleDemonstrators", Tuple.Create(module, users));
        }

        //TODO limit the number of demonstrators it shows
        [HttpPost("ModuleDemonstrators")]
        public IActionResult ModuleDemonstrators(int moduleId)
        {
            var module = _context.CourseUsers
                .SingleOrDefault(m => m.CourseID == moduleId);
            var users = _context.Users.ToList().Where(user => user.Courses.Contains(module));
            return View("ModuleDemonstrators", Tuple.Create(module,users));
        }
        //TODO decide if this is needed
        [HttpGet("ProposeDemonstrator")]
        public IActionResult ProposeDemonstrator()
        {

            return View("ProposeDemonstrator", Tuple.Create(_context.Users.First(), _context.Courses.ToList() ) );
        }

        [HttpPost("ProposeDemonstrator")]
        //TODO figure out these arguments
        public async Task<IActionResult> ProposeDemonstrator(CourseUser model)
        {
            var tup = Tuple.Create(model.Course, _context.Users.ToList());
            if (ModelState.IsValid)//Server side validation
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                
                return View("ModuleDemonstrators", tup);
            }
            //TODO figure out what to pass here, might need model.id
            return View("ModuleDemonstrators", tup);
        }

        [HttpGet("Modules")]
        public IActionResult Modules()
        {
            return View();
        }

        public async Task<IActionResult> DemonstratorDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var demonstrator = _context.Users
                .SingleOrDefault(m => m.UserID == id);
            if (demonstrator == null)
            {
                return NotFound();
            }

            return View(demonstrator);
        }


    }
}
