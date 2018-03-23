using AllocationApp.Data;
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

        public IActionResult AddModule()
        {
            return View("AddModule");
        }
    }
}
