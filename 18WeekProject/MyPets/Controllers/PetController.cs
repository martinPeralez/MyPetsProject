using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPets.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyPets.Controllers
{
    public class PetController : Controller
    {
        //   F i e l d s   &   P r o p e r t i e s

        private IPetRepository _repository;
        private IWebHostEnvironment _webHostEnvironment;
        //   C o n s t r u c t o r s

        public PetController(IPetRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _webHostEnvironment = env;
        }

        //   M e t h o d s

        public IActionResult Index()
        {
            IQueryable<Pet> allPets = _repository.GetAllPets();
            return View(allPets);
        }

        public IActionResult Detail(int id)
        {
            Pet pet = _repository.GetPetById(id);
            if (pet != null)
            {
                return View(pet);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Pet pet = _repository.GetPetById(id);
            if (pet != null)
            {
                return View(pet);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(Pet pet)
        {
            _repository.UpdatePet(pet);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            Pet newPet = new Pet();
            return View(newPet);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Pet pet)
        {
            if (ModelState.IsValid)
            {
                if (pet.CoverPhoto != null)
                {
                    string folder = "books/cover/";
                    folder += Guid.NewGuid().ToString() + "_" + pet.CoverPhoto.FileName;

                    pet.CoverImageUrl = folder;

                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    await pet.CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create)); 

                }
                _repository.Create(pet);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Something Went Wrong.");
                return RedirectToAction("Create", pet);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Pet pet = _repository.GetPetById(id);
            if (pet != null)
            {
                return View(pet);
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(Pet pet, int id)
        {
            _repository.DeletePet(id);
            return RedirectToAction("Index");
        }
    }
}
