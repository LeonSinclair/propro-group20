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
        public IActionResult ProposeDemonstrator(int id)
        {
            var req = Request.Query["id"];

            var course = from courses in _context.Courses
                         select courses;
            var user = from users in _context.Users
                             where users.UserID == id
                             select users;

            ViewBag.courses = course.ToList();
            
            return View("ProposeDemonstrator", user.Single() );
        }

        
        //TODO figure out these arguments
        //just get the IDs and query the DB then
        [HttpPost("ProposeDemonstrator")]
        public async Task<IActionResult> ProposeDemonstrator(int UserID, int CourseID)
        {
            var req = Request.Query;
            var reqForm = Request.Form;
            var course = from courses in _context.Courses
                         where courses.CourseID == CourseID
                         select courses;
            var user = from users in _context.Users
                         where users.UserID == UserID
                         select users;
            User tmpUser = user.Single();
            Course tmpCourse = course.Single();
            CourseUser courseUser = new CourseUser(UserID, tmpUser, CourseID, tmpCourse);

            var tmp = Request.Form;
            if (ModelState.IsValid)//Server side validation
            {
                //TODO set this to send you to the module demonstrators page
                //for now it sends you back to the index
                _context.Add(courseUser);
                await _context.SaveChangesAsync();
                return View("Index", Tuple.Create(_context.Courses.ToList(), _context.Users.ToList()));
            }
            return View("Index", Tuple.Create(_context.Courses.ToList(), _context.Users.ToList()));
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
