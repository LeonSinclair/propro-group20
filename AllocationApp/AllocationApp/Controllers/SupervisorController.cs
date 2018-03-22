using AllocationApp.Data;
using AllocationApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                .SingleOrDefault(m => m.Id == id);
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
                .SingleOrDefault(m => m.Id == id);
            if (subordinate == null)
            {
                return NotFound();
            }

            return View(subordinate);
        }

        [HttpPost("RemoveSubordinate")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subordinate = _context.Subordinates.SingleOrDefault(m => m.Id == id);
            _context.Subordinates.Remove(subordinate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Subordinates));
        }


    }
}
