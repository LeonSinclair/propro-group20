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
            return View("Index",tup);
        }


        //TODO this probably isn't accesible during normal usage, but good to have in case something odd happens 
        
        
        /*
        [HttpGet("ModuleDemonstrators")]
        public IActionResult ModuleDemonstrators()
        {
            var module = _context.Courses.First();
            var users = _context.Users.ToList();
            return View("ModuleDemonstrators", Tuple.Create(module, users));
        }

        //TODO limit the number of demonstrators it shows
        [HttpPost("ModuleDemonstrators")] 
        */
        public IActionResult ModuleDemonstrators(int moduleID)
        {
            var module = _context.Courses
                .SingleOrDefault(m => m.CourseID == moduleID);
            //Select from Users where userID is contained in (Select courses
            //take all users where their userid is in course user and has the right course number
            //basically select all users that have the right courseID
            var query = from user in _context.CourseUsers
                        where user.CourseID == moduleID
                        select user.User;
            

            return View("ModuleDemonstrators", Tuple.Create(module,query.ToList()));
        }
        //TODO decide if this is needed
        [HttpGet("ProposeDemonstrator")]
        public IActionResult ProposeDemonstrator()
        {

            return View("ProposeDemonstrator", Tuple.Create(_context.Users.First(), _context.Courses.ToList() ) );
        }

        [HttpPost("ProposeDemonstrator")]
        //TODO figure out these arguments
        public async Task<IActionResult> ProposeDemonstrator(User user, Course course, int userID, int courseID)
        {
            CourseUser courseUser = new CourseUser();
            courseUser.Course = course;
            courseUser.CourseID = courseID;
            courseUser.User = user;
            courseUser.UserID = userID;
            var tup = Tuple.Create(course, _context.Users.ToList());
            if (ModelState.IsValid)//Server side validation
            {
                
                _context.Add(courseUser);
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
