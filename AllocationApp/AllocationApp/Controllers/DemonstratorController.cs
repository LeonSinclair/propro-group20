using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers
{
    public class DemonstratorController : Controller
    {
        // 
        // GET: /Demonstrator/
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Demonstrator/Welcome/ 
        // Requires using System.Text.Encodings.Web
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;
            return View();
        }

        // 
        // GET: /Demonstrator/Rage/ 
        public string Rage()
        {
            return "Fuck everything.";
        }
    }
}