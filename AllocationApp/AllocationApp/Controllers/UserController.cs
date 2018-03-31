using AllocationApp.Data;
using AllocationApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AllocationApp.Controllers
{
    public class UserController : Controller
    {
        private readonly AllocationContext _context;

        public UserController(AllocationContext context)
        {
            _context = context;
        }

        [HttpGet("Modules")]
        public IActionResult Modules()
        {
            var results = _context.Modules.ToList();
            return View(results);
        }

        [HttpGet("Users")]
        public IActionResult Users()
        {
            var results = _context.Users.ToList();
            return View(results);
        }

        [HttpGet]
        public IActionResult AddModule()
        {
            return View("AddModule");
        }

        [HttpPost]
        public async Task<IActionResult> AddModule(Module model)
        {
            if(ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                var results = _context.Modules.ToList();
                return View("Index", results);
            }
            return View("Index", model);
        }

        [HttpGet("AddUser")]
        public IActionResult AddUser()
        {
            return View("AddUser");
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([Bind("UserID,FirstName,LastName,Occupation")]User model)
        {
            if (ModelState.IsValid)//Server side validation
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return View("UserDetails", model);
            }
            return View("User", model);
        }

        // Personal Settings of a User
        [HttpGet("Settings")]
        public async Task<IActionResult> Settings(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _context.Users
                //.Include(i => i.UserModules)
               .SingleOrDefault(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost("Settings")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SettingsPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userToUpdate = await _context.Users
                 .SingleOrDefaultAsync(c => c.UserID == id);

            if (await TryUpdateModelAsync<User>(userToUpdate,
                "",
        c => c.FirstName, c => c.LastName, c => c.BankName,
            c => c.BankAddress, c => c.IBAN, c => c.SortCode))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction("UserDetails", new {@id=id});
            }
            //PopulateDepartmentsDropDownList(userToUpdate.DepartmentID);

            return View("UserDetails", id);
        }

        // Remove User 
        [HttpGet("RemoveUser")]
        public async Task<IActionResult> RemoveUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _context.Users
               .SingleOrDefault(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost("RemoveUser")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = _context.Users.SingleOrDefault(m => m.UserID == id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Users));
        }

        public async Task<IActionResult> UserDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _context.Users
               .SingleOrDefault(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult AddDemonstratorToModule()
        {
            //var results = from users in _context.Users
            //              join roles in _context.UserRoles on users.UserID equals  into userRole
            //              from demonstrators in userRole
            //              where demonstrators.RoleType == "Demonstrator"
            //              select users;
            //return View(results);
            return View();
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(Role model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                var results = _context.Roles.ToList();
                return View("Roles", results);
            }
            return View("Roles", model);
        }

        [HttpGet]
        public IActionResult AssignRoleToUser(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roles = _context.Roles.ToList();
            if (roles == null)
            {
                return NotFound();
            }
            AddUserToRoleView rolesAndUser = new AddUserToRoleView();
            rolesAndUser.Roles = roles;
            rolesAndUser.UserID = id;
            return View(rolesAndUser);
        }

    }
}
