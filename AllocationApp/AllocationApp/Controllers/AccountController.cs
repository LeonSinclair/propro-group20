using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AllocationApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            //has someone already logged in
            if(this.User.Identity.IsAuthenticated)
            {
                //In case someone is already logged in, then redirect them to action, controller
                return RedirectToAction("Subordinates", "Supervisor");
            }
            return View();
        }
    }
}