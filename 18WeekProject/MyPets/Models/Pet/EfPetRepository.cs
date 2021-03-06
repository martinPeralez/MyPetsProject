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
        private IUserRepository _userRepository;

        //   C o n s t r u c t o r s

        public EfPetRepository(AppDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        //   M e t h o d s

        public IQueryable<Pet> GetAllPets()
        {
            if (_userRepository.IsUserLoggedIn())
            {
                return _context.Pets.Where(p => p.UserId == _userRepository.GetLoggedInUserId());
            }
            Pet[] noPets = new Pet[0];
            return noPets.AsQueryable<Pet>();
        }
        public Pet GetPetById(int petId)
        {
            return _context.Pets.Where(p => p.Id == petId).FirstOrDefault();
            //return await _context.Pets.Where(p => p.Id == petId).Select(pet => new Pet()
            //{
            //    Gallery = pet.PetGallery.Select(g => new PetGallery()
            //    {
            //        Id = g.Id,
            //        Name = g.Name,
            //        Url = g.Url
            //    }).ToList()
            //}).FirstOrDefaultAsync();
        }

        public string GetPetCoverUrl(int id)
        {
            Pet pet = _context.Pets.Where(p => p.Id == id).FirstOrDefault();
            char[] charArr = pet.CoverImageUrl.ToCharArray();

            for (int i = 0; i < 1; i++)
            {
                if (charArr[0] == '/')
                {
                    return pet.CoverImageUrl;
                }
                else
                {
                    return '/' + pet.CoverImageUrl;
                }
            }
            return '/' + pet.CoverImageUrl;
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
                petToUpdate.SpayNeuter = pet.SpayNeuter;
                _context.SaveChanges();
            }

            return petToUpdate;
        }

        public Pet Create(Pet pet)
        {
            if (_userRepository.IsUserLoggedIn())
            {
                try
                {
                    pet.UserId = _userRepository.GetLoggedInUserId();

                    pet.Url = new List<string>();
                    string url = pet.CoverImageUrl;
                    pet.Url.Add(url);
                    _context.Pets.Add(pet);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                }
                return pet;
            }

            return null;
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
