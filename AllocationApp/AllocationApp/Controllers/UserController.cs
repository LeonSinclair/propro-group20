using AllocationApp.Data;
using AllocationApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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
            var tup = Tuple.Create(_context.Modules.ToList(), _context.Users.ToList());
            return View(tup);
        }

        [Authorize]
        [HttpGet("Users")]
        public IActionResult Users()
        {
            var results = _context.Users.ToList();
            return View(results);
        }

        [HttpGet("Demonstrator")]
        public IActionResult Demonstrator()
        {
            var results = _context.Users.ToList();
            return View(results);
        }

        [HttpGet("Superuser")]
        public IActionResult Superuser()
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
            return View("AddRole");
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

        [HttpGet("AssignRoleToUser")]
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
            rolesAndUser.User = _context.Users.Find(id);
            return View("AssignRoleToUser", rolesAndUser);
        }

        [HttpPost("AssignRoleToUser")]
        public async Task<IActionResult> AssignRoleToUser(int UserID, int RoleID)
        {
            var role = from roles in _context.Roles
                       where roles.RoleID == RoleID
                       select roles;
            var user = from users in _context.Users
                       where users.UserID == UserID
                       select users;

            var userDisplay = from users in _context.UserRoles
                              where users.RoleID == RoleID
                              select users.User;
            UserRole userRole = new UserRole();
            userRole.UserID = UserID;
            userRole.User = _context.Users.Find(UserID);
            userRole.RoleID = RoleID;
            userRole.Role = _context.Roles.Find(RoleID);
            _context.Entry(userRole).State = EntityState.Added;
            //TODO catch exception from them already demoing for the module
            if (ModelState.IsValid)
            {
                _context.UserRoles.Add(userRole);
                await _context.SaveChangesAsync();
                //TODO query to find correct users
                return View("UserDetails", _context.Users.Find(UserID));
            }
            return View("UserDetails", _context.Users.Find(UserID));
        }

    }
}
