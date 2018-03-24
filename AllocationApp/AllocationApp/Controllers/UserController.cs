using AllocationApp.Data;
using AllocationApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllocationApp.Controllers
{
    public class UserController : Controller
    {
        private readonly AllocationContext _context;

        public UserController(AllocationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var results = _context.Module.ToList();
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
                var results = _context.Module.ToList();
                return View("Index", results);
            }
            return View("Index", model);
        }
    }
}
