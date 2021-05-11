using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPets.Models
{
    public interface IPetRepository
    {
        public IQueryable<Pet> GetAllPets();
    }
}
