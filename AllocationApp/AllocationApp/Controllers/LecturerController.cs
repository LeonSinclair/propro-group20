using AllocationApp.Data;
using AllocationApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllocationApp.Controllers
{

    //TODO implement All Demonstrators button
    //TODO implement All Modules Button
    //TODO search function
    //TODO fix module demonstrator book
    //TODO fix names
    //TODO delete proposals or set them as not approved and not query for them
    //TODO query users for role
    //TODO delete Module, rename course to Module, fix everything
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

        
        
       
        public IActionResult ModuleDemonstrators(int moduleID)
        {
            var module = _context.Courses
                .SingleOrDefault(m => m.CourseID == moduleID);
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

        
        [HttpPost("ProposeDemonstrator")]
        public async Task<IActionResult> ProposeDemonstrator(int UserID, int CourseID)
        {
            var course = from courses in _context.Courses
                         where courses.CourseID == CourseID
                         select courses;
            var user = from users in _context.Users
                         where users.UserID == UserID
                         select users;
            Proposal proposal = new Proposal();
            proposal.UserID = UserID;
            proposal.User = _context.Users.Find(UserID);
            proposal.CourseID = CourseID;
            proposal.Course = _context.Courses.Find(CourseID);
            proposal.Approved = false;
            _context.Entry(proposal).State = EntityState.Added;
            //TODO catch exception from them already demoing for the module
            if (ModelState.IsValid)
            {
                _context.Proposal.Add(proposal);
                await _context.SaveChangesAsync();
                //TODO query to find correct users
                return View("ModuleDemonstrators", Tuple.Create(_context.Courses.Find(CourseID), _context.Users.ToList()));
            }
            return View("ModuleDemonstrators", Tuple.Create(_context.Courses.Find(CourseID), _context.Users.ToList()));
        }


        public async Task<IActionResult> ConfirmProposal(int UserID, int CourseID)
        {
            //TODO check input data here
            var proposal = from proposals in _context.Proposal
                           where proposals.UserID == UserID && proposals.CourseID == CourseID
                           select proposals;

            Proposal tmpProposal = proposal.Single();
            tmpProposal.Approved = true;
            User tmpUser = _context.Users.Find(tmpProposal.UserID);
            Course tmpCourse = _context.Courses.Find(tmpProposal.CourseID);
            CourseUser courseUser = new CourseUser(UserID, tmpUser, CourseID, tmpCourse);
            //TODO catch exception from them already demoing for the module
            if (ModelState.IsValid)
            {
                //TODO set this to send you to the module demonstrators page
                //for now it sends you back to the index

                _context.CourseUsers.Add(courseUser);
                await _context.SaveChangesAsync();
                return View("ModuleDemonstrators", Tuple.Create(tmpCourse, _context.Users.ToList()));
            }
            return View("ModuleDemonstrators", Tuple.Create(tmpCourse, _context.Users.ToList()));
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

        //TODO when clicked these should return a link to the appropriate module or demonstrator page
        public IActionResult AllModules()
        {

            return View(_context.Courses.ToList());
        }

        public IActionResult AllDemonstrators()
        {

            return View(_context.Users.ToList());
        }

        public IActionResult ViewProposals()
        {


            return View(_context.Proposal.ToList());
        }

    }
}
