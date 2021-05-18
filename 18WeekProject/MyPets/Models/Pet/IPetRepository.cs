using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPets.Models
{
    public interface IPetRepository
    {
        //  C r e a t e

        public Pet Create(Pet pet);

        //   R e a d
        public IQueryable<Pet> GetAllPets();
        public Pet GetPetById(int petId);
        public string GetPetCoverUrl(int id);
        //   U p d a t e
        public Pet UpdatePet(Pet pet);

        //   D e l e t e

        public bool DeletePet(int id);
    }
}
