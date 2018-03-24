using AllocationApp.Data;
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
            if (ModelState.IsValid)//Server side validation
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

        [HttpGet("Settings")]
        public async Task<IActionResult> Settings(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Subordinates
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            
            if (user == null)
            {
                return NotFound();
            }

            return View("Settings");
        }

        [HttpPost, ActionName("Settings")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SettingsPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Subordinates
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            
            if (await TryUpdateModelAsync<Subordinates>(
                user,
                "",
                i => i.FirstName, i => i.LastName))
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
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public IActionResult BankDetails()
        {
            ViewData["Message"] = "Your Dashboard page.";

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

        // Bank Details - NOT WORKING YET

        [HttpGet("AddBankDetails")]
        public IActionResult BankDetails(int? id)
        {
            var subordinate = _context.Subordinates.SingleOrDefault(m => m.ID == id);

            return View("BankDetails");
        }

        [HttpPost("AddBankDetails")]
        public async Task<IActionResult> BankDetails([Bind("ID,BankName,BankAddress,IBAN,SortCode")]Subordinates model)
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
