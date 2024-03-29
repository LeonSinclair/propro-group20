﻿using AllocationApp.Data;
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
    //TODO only show modules where the lecturer is the current lecturer
    //if that is done via Users having a role then it shouldn't be a problem
    public class LecturerController : Controller
    {
        private readonly AllocationContext _context;


        public LecturerController(AllocationContext context)
        {
            _context = context;
            
        }

        public IActionResult Index()
        {
            var tup = Tuple.Create(_context.Modules.ToList(), _context.Users.ToList());
            return View(tup);
        }

        public IActionResult LecturerDashboard()
        {
            return RedirectToAction(nameof(Index));
        }
       
        public IActionResult ModuleDemonstrators(int moduleID)
        {
            var module = _context.Modules
                .SingleOrDefault(m => m.ModuleID == moduleID);
            var query = from user in _context.ModuleUsers
                        where user.ModuleID == moduleID
                        select user.User;
            

            return View("ModuleDemonstrators", Tuple.Create(module,query.ToList()));
        }
        //TODO decide if this is needed
        [HttpGet("ProposeDemonstrator")]
        public IActionResult ProposeDemonstrator(int id)
        {
           
            var moduleQuery = from modules in _context.Modules
                         select modules;
            var user = from users in _context.Users
                             where users.UserID == id
                             select users;

            ViewBag.modules = moduleQuery.ToList();
            
            return View("ProposeDemonstrator", user.Single() );
        }

        
        [HttpPost("ProposeDemonstrator")]
        public async Task<IActionResult> ProposeDemonstrator(int UserID, int ModuleID)
        {
            var module = from modules in _context.Modules
                         where modules.ModuleID == ModuleID
                         select modules;
            var user = from users in _context.Users
                         where users.UserID == UserID
                         select users;

            var userDisplay = from users in _context.ModuleUsers
                              where users.ModuleID == ModuleID
                              select users.User;
            Proposal proposal = new Proposal();
            proposal.UserID = UserID;
            proposal.User = _context.Users.Find(UserID);
            proposal.ModuleID = ModuleID;
            proposal.Module = _context.Modules.Find(ModuleID);
            proposal.Approved = false;
            _context.Entry(proposal).State = EntityState.Added;
            //TODO catch exception from them already demoing for the module
            if (ModelState.IsValid)
            {
                _context.Proposal.Add(proposal);
                await _context.SaveChangesAsync();
                //TODO query to find correct users
                return View("ModuleDemonstrators", Tuple.Create(_context.Modules.Find(ModuleID), userDisplay.ToList()));
            }
            return View("ModuleDemonstrators", Tuple.Create(_context.Modules.Find(ModuleID), userDisplay.ToList()));
        }


        public async Task<IActionResult> ConfirmProposal(int UserID, int ModuleID)
        {
            var proposal = from proposals in _context.Proposal
                           where proposals.UserID == UserID && proposals.ModuleID == ModuleID
                           select proposals;

            Proposal tmpProposal = proposal.Single();
            tmpProposal.Approved = true;
            User tmpUser = _context.Users.Find(tmpProposal.UserID);
            Module tmpModule = _context.Modules.Find(tmpProposal.ModuleID);
            ModuleUser moduleUser = new ModuleUser(UserID, tmpUser, ModuleID, tmpModule);
            var userDisplay = from users in _context.ModuleUsers
                              where users.ModuleID == ModuleID
                              select users.User;
            //TODO catch exception from them already demoing for the module
            if (ModelState.IsValid)
            {
                
                _context.ModuleUsers.Add(moduleUser);
                await _context.SaveChangesAsync();
                return View("ModuleDemonstrators", Tuple.Create(tmpModule, userDisplay.ToList()));
            }
            return View("ModuleDemonstrators", Tuple.Create(tmpModule, userDisplay.ToList()));
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

            return View(_context.Modules.ToList());
        }

        public IActionResult AllDemonstrators()
        {

            return View(_context.Users.ToList());
        }

        [HttpGet("AddSkillsToModule")]
        public IActionResult AddSkillsToModule(int id)
        {
            var module = from modules in _context.Modules
                         where modules.ModuleID == id
                         select modules;
            return View(module.Single());
        }
        [HttpPost("AddSkillsToModule")]
        public async Task<IActionResult> AddSkillsToModule(int moduleID, String skillName )
        {
            var module = from modules in _context.Modules
                         where modules.ModuleID == moduleID
                         select modules;
            Skill newSkill;
            //TODO check skill doesn't exist already
            try
            {
                newSkill = new Skill(_context.Skills.Last().SkillID + 1, skillName);
            }
            catch(Exception e)
            {
                newSkill = new Skill(1, skillName);
            }
            try
            {
                var tmp = module.First();
                tmp.SkillRequirements.Add(newSkill);
                _context.Entry(tmp).State = EntityState.Modified;

            }
            catch (Exception e)
            {
                module.First().SkillRequirements = new List<Skill>();
                module.First().SkillRequirements.Add(newSkill);
            }
            _context.Skills.Add(newSkill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        public IActionResult ViewProposals()
        {
            IList<Proposal> pList = _context.Proposal.Include(s => s.User).Include(m => m.Module).ToList<Proposal>();
            return View(_context.Proposal.ToList());
        }

    }
}
