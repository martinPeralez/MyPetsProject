using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyPets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPets.Controllers
{
    public class HomeController : Controller
    {
        //   F i e l d s   &   P r o p e r t i e s

        private readonly ILogger<HomeController> _logger;
        private IEmailRepository _emailRepo;

        //   C o n s t r u c t o r s

        public HomeController(ILogger<HomeController> logger, IEmailRepository emailRepo)
        {
            _logger = logger;
            _emailRepo = emailRepo;
        }

        //   M e t h o d s

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SendEmail()
        {
            _emailRepo.Send("guillermop@live.com", "You Jun Da Tae Simp", "Is Pres.");
            return RedirectToAction("Index");
        }
    }
}
