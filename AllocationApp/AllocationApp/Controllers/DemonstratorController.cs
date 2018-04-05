using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using AllocationApp.Data;
using AllocationApp.Models;
using Microsoft.EntityFrameworkCore;

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
            var umhList = _context.ModuleUsers.ToList();

            return View(umhList);
        }

        // GET: Demonstrator/LogHours
        [HttpGet("LogHours")]
        public IActionResult LogHoursModuleSelect(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var modules = _context.Modules.ToList();
            //Gets all the modulesUser table entries that the provided user is associated with
            var modulesUsers = from mU in _context.ModuleUsers
                          where mU.UserID == id
                          select mU;
            List<Module> modules = new List<Module>();
            //retrieves the moduleUser table entries related module objects
            foreach(var item in modulesUsers)
            {
                modules.Add(_context.Modules.Find(item.ModuleID));
            }
            if (modules == null)
            {
                return NotFound();
            }
            LogHoursView modulesAndUser = new LogHoursView();
            modulesAndUser.Modules = modules;
            modulesAndUser.User = _context.Users.Find(id);
            return View("LogHours", modulesAndUser);
        }

        [HttpPost("LogHours")]
        public IActionResult LogHoursModuleSelect(int UserID, int ModuleID)
        {
            var moduleUsers = from mU in _context.ModuleUsers
                       where (mU.ModuleID == ModuleID) && (mU.UserID == UserID)
                       select mU;
            ModuleUser moduleUser = new ModuleUser();
            moduleUser.Module = _context.Modules.Find(ModuleID);
            moduleUser.ModuleID = ModuleID;
            moduleUser.User = _context.Users.Find(UserID);
            moduleUser.UserID = UserID;
            foreach(var item in moduleUsers)
            {
                if(item.ModuleID == ModuleID && item.UserID == UserID)
                {
                    moduleUser.HoursAllocated = item.HoursAllocated;
                    moduleUser.HoursWorked = item.HoursWorked;
                }
            }
            return View("LogHoursFinal", moduleUser);
        }

        [HttpPost("LogHoursFinal")]
        public async Task<IActionResult> LogHoursFinal(int UserID, int ModuleID, double hoursWorked, double hoursAllocated)
        {
            ModuleUser moduleUser = _context.ModuleUsers.Find(ModuleID, UserID);
            moduleUser.HoursWorked = moduleUser.HoursWorked + hoursWorked;
            _context.Entry(moduleUser).State = EntityState.Added;
                    //TODO catch exception from them already demoing for the module
                    if (ModelState.IsValid)
                    {
                        _context.ModuleUsers.Update(moduleUser);
                        await _context.SaveChangesAsync();
                //Redirects to the "Users" action of the controller "User"
                return RedirectToAction("Users", "User");
                    }
            return RedirectToAction("Users", "User");
        }



        //welcome() method should be deleted from final version of project
        // GET: Demonstrator/Welcome
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;
            return View();
        }
    }
}