using Microsoft.AspNetCore.Mvc;
using MyPets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPets.Controllers
{
    public class UserController : Controller
    {
        //   F i e l d s   &   P r o p e r t i e s

        private IUserRepository _repository;

        //   C o n s t r u c t o r s

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        //   M e t h o d s

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserRegistrationViewModel urvm)
        {
            //if (urvm.Password != urvm.VerificationPassword)
            //{
            //    ModelState.AddModelError("", "Passwords did not match.");
            //}
            if (ModelState.IsValid)
            {
                User u = new User();
                u.IsAdmin = false;
                u.Password = urvm.Password;
                u.Email = urvm.Email;
                u.FirstName = urvm.FirstName;
                u.LastName = urvm.LastName;
                u.PhoneNumber = urvm.PhoneNumber;
                u.Address = urvm.Address;
                User newUser = _repository.Create(u);
                if (newUser == null)
                {
                    ModelState.AddModelError("", "This User Already Exists.");
                    return View(urvm);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Could not register this account. Try again.");
            return View(urvm);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User u)
        {
            bool loggedIn = _repository.Login(u);
            if (loggedIn == true)
            {
                return RedirectToAction("Index", "Pet");
            }
            ModelState.AddModelError("", "Could not log this user in. Try again.");
            return View(u);
        }
        public IActionResult Logout()
        {
            _repository.Logout();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Update()
        {
            User user = _repository.GetUserById(_repository.GetLoggedInUserId());
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(User user)
        {
            _repository.Update(user);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(UserChangePasswordViewModel ucpvm)
        {
            if (ModelState.IsValid)
            {
                bool success = _repository.ChangePassword(ucpvm.CurrentPassword, ucpvm.NewPassword);
                if (success == true)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Unable To Change Password.");
                return View(ucpvm);
            }
            return View(ucpvm);
        }
    }   
}
