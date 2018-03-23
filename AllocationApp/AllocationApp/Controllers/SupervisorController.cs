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
        public async Task<IActionResult> AddSubordinates([Bind("Id,firstname,surname,occupation")]Subordinates model)
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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Subordinates
                .Include(i => i.firstname)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Subordinates
                .Include(i => i.Firstname)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);

            if (await TryUpdateModelAsync<Subordinates>(
                user,
                "",
                i => i.Firstname, i => i.surname))
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



    }
}
