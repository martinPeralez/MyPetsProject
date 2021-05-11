using Microsoft.AspNetCore.Mvc;
using MyPets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPets.Controllers
{
    public class PetController : Controller
    {
        //   F i e l d s   &   P r o p e r t i e s

        private IPetRepository _repository;

        //   C o n s t r u c t o r s

        public PetController(IPetRepository repository)
        {
            _repository = repository;
        }

        //   M e t h o d s

        public IActionResult Index()
        {
            IQueryable<Pet> allPets = _repository.GetAllPets();
            return View(allPets);
        }
    }
}
