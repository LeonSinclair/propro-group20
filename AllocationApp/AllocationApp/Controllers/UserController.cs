﻿using AllocationApp.Data;
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
            return View("Modules", results);
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

        [HttpGet("AssignUserToModule")]
        public IActionResult AssignUserToModule(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modules = _context.Modules.ToList();
            if (modules == null)
            {
                return NotFound();
            }
            AddUserToModuleView modulesAndUser = new AddUserToModuleView();
            modulesAndUser.Modules = modules;
            modulesAndUser.User = _context.Users.Find(id);
            return View("AssignUserToModule", modulesAndUser);
        }

        [HttpPost("AssignUserToModule")]
        public async Task<IActionResult> AssignUserToModule(int UserID, int ModuleID)
        {
            var role = from modules in _context.Modules
                       where modules.ModuleID == ModuleID
                       select modules;
            var user = from users in _context.Users
                       where users.UserID == UserID
                       select users;

            var userDisplay = from users in _context.ModuleUsers
                              where users.ModuleID == ModuleID
                              select users.User;
            ModuleUser userModule = new ModuleUser();//UserID, _context.Users.Find(UserID), ModuleID, _context.Modules.Find(ModuleID));
            userModule.UserID = UserID;
            userModule.User = _context.Users.Find(UserID);
            userModule.ModuleID = ModuleID;
            userModule.Module = _context.Modules.Find(ModuleID);
            //default hours to be allocated is 6
            userModule.HoursAllocated = 6.0;
            userModule.HoursWorked = 0.0;
            userModule.HoursPaid = 0.0;
            userModule.HourlyPayRate = 21.50;
            _context.Entry(userModule).State = EntityState.Added;
            //TODO catch exception from them already demoing for the module
            if (ModelState.IsValid)
            {
                _context.ModuleUsers.Add(userModule);
                await _context.SaveChangesAsync();
                //TODO query to find correct users
                return View("UserDetails", _context.Users.Find(UserID));
            }
            return View("UserDetails", _context.Users.Find(UserID));
        }

        [HttpGet("AssignModuleToUser")]
        public IActionResult AssignModuleToUser(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = _context.Users.ToList();
            if (users == null)
            {
                return NotFound();
            }
            AddModuleToUserView usersAndModules = new AddModuleToUserView();
            usersAndModules.Users = users;
            usersAndModules.Module = _context.Modules.Find(id);
            return View("AssignModuleToUser", usersAndModules);
        }

        [HttpPost("AssignModuleToUser")]
        public async Task<IActionResult> AssignModuleToUser(int UserID, int ModuleID)
        {
            var role = from modules in _context.Modules
                       where modules.ModuleID == ModuleID
                       select modules;
            var user = from users in _context.Users
                       where users.UserID == UserID
                       select users;

            var userDisplay = from users in _context.ModuleUsers
                              where users.ModuleID == ModuleID
                              select users.User;
            ModuleUser userModule = new ModuleUser();//UserID, _context.Users.Find(UserID), ModuleID, _context.Modules.Find(ModuleID));
            userModule.UserID = UserID;
            userModule.User = _context.Users.Find(UserID);
            userModule.ModuleID = ModuleID;
            userModule.Module = _context.Modules.Find(ModuleID);
            _context.Entry(userModule).State = EntityState.Added;
            //TODO catch exception from them already demoing for the module
            if (ModelState.IsValid)
            {
                _context.ModuleUsers.Add(userModule);
                await _context.SaveChangesAsync();
                //TODO query to find correct users
                return View("UserDetails", _context.Users.Find(UserID));
            }
            return View("UserDetails", _context.Users.Find(UserID));
        }

        [HttpGet("CalculatePay")]
        public IActionResult CalculatePay(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModules = from modules in _context.ModuleUsers
                              where modules.UserID == id
                              select modules;
            if (userModules == null)
            {
                return NotFound();
            }
            double total = 0.0;
            foreach(var module in userModules)
            {
                if(module.HoursWorked > module.HoursPaid)
                {
                    double unPaidHours = module.HoursWorked - module.HoursPaid;
                    total += (module.HourlyPayRate * unPaidHours);
                }
            }
            UserPayView payView = new UserPayView();
            payView.User = _context.Users.Find(id);
            payView.UnPaidSum = total;
            payView.UserID = id;
            return View("AssignUserToModule");
        }

        //[HttpPost("AssignUserToModule")]
        //public async Task<IActionResult> AssignUserToModule(int UserID, int ModuleID)
        //{
        //    var role = from modules in _context.Modules
        //               where modules.ModuleID == ModuleID
        //               select modules;
        //    var user = from users in _context.Users
        //               where users.UserID == UserID
        //               select users;

        //    var userDisplay = from users in _context.ModuleUsers
        //                      where users.ModuleID == ModuleID
        //                      select users.User;
        //    ModuleUser userModule = new ModuleUser();//UserID, _context.Users.Find(UserID), ModuleID, _context.Modules.Find(ModuleID));
        //    userModule.UserID = UserID;
        //    userModule.User = _context.Users.Find(UserID);
        //    userModule.ModuleID = ModuleID;
        //    userModule.Module = _context.Modules.Find(ModuleID);
        //    //default hours to be allocated is 6
        //    userModule.HoursAllocated = 6.0;
        //    userModule.HoursWorked = 0.0;
        //    userModule.HoursPaid = 0.0;
        //    userModule.HourlyPayRate = 21.50;
        //    _context.Entry(userModule).State = EntityState.Added;
        //    //TODO catch exception from them already demoing for the module
        //    if (ModelState.IsValid)
        //    {
        //        _context.ModuleUsers.Add(userModule);
        //        await _context.SaveChangesAsync();
        //        //TODO query to find correct users
        //        return View("UserDetails", _context.Users.Find(UserID));
        //    }
        //    return View("UserDetails", _context.Users.Find(UserID));
        //}

        [HttpGet("ModuleDetails")]
        public async Task<IActionResult> ModuleDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var module = _context.Modules
               //.Include(i => i.UserModules)
               .SingleOrDefault(m => m.ModuleID == id);
            if (module == null)
            {
                return NotFound();
            }

            return View("ModuleDetails", module);
        }

    }
}
