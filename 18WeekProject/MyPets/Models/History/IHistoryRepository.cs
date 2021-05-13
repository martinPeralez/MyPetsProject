using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPets.Models
{
    public interface IHistoryRepository
    {
        //  C r e a t e

        public History Create(History history);

        //   R e a d
        public IQueryable<History> GetAllHistories();
        public History GetHistoryById(int historyId);

        //   U p d a t e
        public History UpdateHistory(History history);

        //   D e l e t e

        public bool DeleteHistory(int id);
    }
}
