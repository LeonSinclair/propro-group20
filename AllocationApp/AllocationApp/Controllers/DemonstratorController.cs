using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using AllocationApp.Data;

namespace AllocationApp.Controllers
{
    public class DemonstratorController : Controller
    {
        private readonly AllocationContext _context;

        public DemonstratorController(AllocationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var umhList = _context.UserModuleHours.ToList();

            return View(umhList);
        }

        // GET: Demonstrator/LogHours
        public IActionResult LogHours()
        {
            var umh = _context.UserModuleHours.First();
            var userList = _context.Users.ToList();
            var moduleList = _context.Modules.ToList();

            return View(Tuple.Create(umh, userList, moduleList));
        }

        //welcome() method should be deleted from final version of project
        // GET: Demonstrator/Welcome
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;
            return View();
        }
    }
}