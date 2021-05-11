using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPets.Models
{
    public class EfPetRepository : IPetRepository
    {
        //   F i e l d s   &   P r o p e r t i e s

        private AppDbContext _context;

        //   C o n s t r u c t o r s

        public EfPetRepository(AppDbContext context)
        {
            _context = context;
        }

        //   M e t h o d s

        public IQueryable<Pet> GetAllPets()
        {
            return _context.Pets;
        }
        public Pet GetPetById(int petId)
        {
            return _context.Pets.Where(p => p.Id == petId).FirstOrDefault();
        }

        public Pet UpdatePet(Pet pet)
        {
            Pet petToUpdate = _context.Pets.SingleOrDefault(p => p.Id == pet.Id);
            if (petToUpdate != null)
            {
                petToUpdate.Name = pet.Name;
                petToUpdate.Species = pet.Species;
                petToUpdate.Age = pet.Age;
                petToUpdate.Color = pet.Color;
                petToUpdate.Breed = pet.Breed;
                petToUpdate.Sex = pet.Sex;
                _context.SaveChanges();
            }

            return petToUpdate;
        }

        public Pet Create(Pet pet)
        {
            _context.Pets.Add(pet);
            _context.SaveChanges();
            return pet;
        }

        public bool DeletePet(int id)
        {
            Pet petToDelete = _context.Pets.Find(id);
            if (petToDelete == null)
            {
                return false;
            }

            _context.Pets.Remove(petToDelete);
            _context.SaveChanges();
            return true;
        }
    }
}
