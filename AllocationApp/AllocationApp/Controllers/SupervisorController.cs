﻿using AllocationApp.Data;
using AllocationApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AllocationApp.Controllers
{
    public class SupervisorController : Controller
    {
        private readonly AllocationContext _context;
        
        public SupervisorController(AllocationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Subordinates")]
        public IActionResult Subordinates()
        {
            var results = _context.Subordinates.ToList();
            return View(results);
        }

        [HttpGet("AddSubordinates")]
        public IActionResult AddSubordinates()
        {
            return View("AddSubordinate");
        }

        [HttpPost("AddSubordinates")]
        public async Task<IActionResult> AddSubordinates([Bind("ID,FirstName,LastName,Occupation")]Subordinates model)
        {
            if(ModelState.IsValid)//Server side validation
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return View("SubordinateDetails", model);
            }
            return View("Subordinates", model);
        }

        [HttpGet("Modules")]
        public IActionResult Modules()
        {
            return View();
        }

        public async Task<IActionResult> SubordinateDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subordinate = _context.Subordinates
                .SingleOrDefault(m => m.ID == id);
            if (subordinate == null)
            {
                return NotFound();
            }

            return View(subordinate);
        }

        [HttpGet("RemoveSubordinate")]
        public async Task<IActionResult> RemoveSubordinate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subordinate = _context.Subordinates
                .SingleOrDefault(m => m.ID == id);
            if (subordinate == null)
            {
                return NotFound();
            }

            return View(subordinate);
        }

        [HttpPost("RemoveSubordinate")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subordinate = _context.Subordinates.SingleOrDefault(m => m.ID == id);
            _context.Subordinates.Remove(subordinate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Subordinates));
        }

        [HttpGet]
        public async Task<IActionResult> EditSubordinate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Subordinates
                .Include(i => i.SubordinateModules)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);

            var modules = from m in _context.Module
                          select new
                          {
                              m.ModuleID,
                              m.Name,
                              Checked = ((from sb in _context.SubordinateModules
                                          where (sb.SubordinateID == id) & (sb.ModuleID == m.ModuleID)
                                          select sb).Count() > 0)
                          };

            var subordinate = new Subordinates();
            subordinate.ID = id.Value;
            subordinate.FirstName = user.FirstName;
            subordinate.LastName = user.LastName;

            var CheckboxList = new List<CheckboxViewModel>();
            foreach(var module in modules)
            {
                CheckboxList.Add(new CheckboxViewModel { ID = module.ModuleID, ModuleName = module.Name, Checked = module.Checked});
            }

            subordinate.Modules = CheckboxList;

            if (user == null)
            {
                return NotFound();
            }
            return View(subordinate);
            // (user)
        }

        [HttpPost, ActionName("EditSubordinate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSubordinate(int? id, Subordinates subordinate)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var user = await _context.Subordinates
            //    .Include(i => i.SubordinateModules)
            //    .AsNoTracking()
            //    .SingleOrDefaultAsync(m => m.ID == id);

            //if (await TryUpdateModelAsync<Subordinates>(
            //    user,
            //    "",
            //    i => i.FirstName, i => i.LastName))
            //{
            if(ModelState.IsValid)
            { 
                try
                {
                    _context.Subordinates.Update(subordinate);
                    await _context.SaveChangesAsync();

                    foreach(var item in _context.SubordinateModules)
                    {
                        if (item.SubordinateID == subordinate.ID)
                        {
                            //_context.Entry(item).State = System.Data.Ent
                        }
                    }
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Subordinates));
            }
            return View(subordinate);
        }

        // Bank Details

        [HttpGet("AddBankDetails")]
        public IActionResult AddBankDetails()
        {
            return View("BankDetails");
        }

        [HttpPost("AddBankDetails")]
        public async Task<IActionResult> AddBankDetails([Bind("BankName,BankAddress,IBAN,SortCode")]Subordinates model)
        {
            if(ModelState.IsValid)//Server side validation
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return View("BankDetails", model);
            }
            return View("Subordinates", model);
        }


    }
}
