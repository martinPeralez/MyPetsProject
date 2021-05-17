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
        private IPetRepository _petRepository;

        //   C o n s t r u c t o r s

        public HomeController(ILogger<HomeController> logger, IEmailRepository emailRepo, IPetRepository petRepository)
        {
            _logger = logger;
            _emailRepo = emailRepo;
            _petRepository = petRepository;
        }

        //   M e t h o d s

        public IActionResult Index()
        {
            IQueryable<Pet> allPets = _petRepository.GetAllPets();
            return View(allPets);
        }
        public IActionResult SendEmail()
        {
            _emailRepo.Send("guillermop@live.com", "You Jun Da Tae Simp", "Is Pres.");
            return RedirectToAction("Index");
        }
    }
}
