using Microsoft.AspNetCore.Mvc;
using MyPets.Models;
using System.Linq;

namespace MyPets.Controllers
{
    public class HistoryController : Controller
    {
        //   F i e l d s   &   P r o p e r t i e s

        private IHistoryRepository _repository;
        private IPetRepository _petRepository;

        //   C o n s t r u c t o r s

        public HistoryController(IHistoryRepository repository, IPetRepository petRepository)
        {
            _repository = repository;
            _petRepository = petRepository;
        }

        //   M e t h o d s

        public IActionResult Index()
        {
            IQueryable<History> allHistories = _repository.GetAllHistories();
            return View(allHistories);
        }

        public IActionResult Detail(int id)
        {
            History history = _repository.GetHistoryById(id);
            if (history != null)
            {
                return View(history);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            History history = _repository.GetHistoryById(id);
            if (history != null)
            {
                return View(history);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(History history)
        {
            _repository.UpdateHistory(history);
            return RedirectToAction("Detail", "Pet", new { id = history.PetId});
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            History newHistory = new History();
            newHistory.PetId = id;
            return View(newHistory);
        }
        [HttpPost]
        public IActionResult Create(History history)
        {
            history.Id = 0;
            _repository.Create(history);
            Pet addPetHistory = _petRepository.GetPetById(history.PetId);
            addPetHistory.Histories.Add(history);
            return RedirectToAction("Detail", "Pet", _petRepository.GetPetById(history.PetId));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            History history = _repository.GetHistoryById(id);
            if (history != null)
            {
                return View(history);
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(History history, int id)
        {
            _repository.DeleteHistory(id);
            return RedirectToAction("Detail", "Pet", new { id = history.PetId });
        }
    }
}
