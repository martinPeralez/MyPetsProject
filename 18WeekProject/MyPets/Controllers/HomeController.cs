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
        private IUserRepository _userRepository;

        //   C o n s t r u c t o r s

        public HomeController(ILogger<HomeController> logger, IEmailRepository emailRepo, IPetRepository petRepository, IUserRepository userRepository)
        {
            _logger = logger;
            _emailRepo = emailRepo;
            _petRepository = petRepository;
            _userRepository = userRepository;
        }

        //   M e t h o d s

        public IActionResult Index()
        {
            IQueryable<Pet> allPets = _petRepository.GetAllPets();
            return View(allPets);
        }
        public IActionResult SendEmail(string email)
        {
            string randomPassword = _userRepository.GenerateRandomPassword();
            _emailRepo.Send(email, "Use this random password to login.", randomPassword);
            User userPassword = _userRepository.GetUserByEmailAddress(email);
            userPassword.Password = randomPassword;
            _userRepository.Update(userPassword);
            return RedirectToAction("Index");
        }
    }
}
