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

        [HttpPost("ModuleDemonstrators")]
        public IActionResult ModuleDemonstrators(int moduleId)
        {
            var module = _context.Courses
                .SingleOrDefault(m => m.CourseID == moduleId);
            var users = _context.CourseUsers.Where(courseuser => courseuser.CourseID == moduleId);
            return View(Tuple.Create(module,users));
        }
        //TODO decide if this is needed
        [HttpGet("ProposeDemonstrator")]
        public IActionResult ProposeDemonstrator()
        {
            return View("ProposeDemonstrator", Tuple.Create(_context.Users,_context.Courses.ToList()));
        }

        [HttpPost("ProposeDemonstrator")]
        //TODO figure out these arguments
        public async Task<IActionResult> ProposeDemonstrator([Bind("Id,firstname,surname,occupation")]User model)
        {
            if (ModelState.IsValid)//Server side validation
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return View("ProposeDemonstrator", model);
            }
            //TODO figure out what to pass here, might need model.id
            return View("ModuleDemonstrators", model);
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
                .SingleOrDefault(m => m.ID == id);
            if (demonstrator == null)
            {
                return NotFound();
            }

            return View(demonstrator);
        }


    }
}
