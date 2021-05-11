using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPets.Models.Pet
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
    }
}
