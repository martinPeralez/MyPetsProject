using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPets.Models
{
    public class EfHistoryRepository : IHistoryRepository
    {
        //   F i e l d s   &   P r o p e r t i e s

        private AppDbContext _context;
        private IUserRepository _userRepository;
        //   C o n s t r u c t o r s

        public EfHistoryRepository(AppDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        //   M e t h o d s

        //  C r e a t e

        public History Create(History history)
        {
            if (_userRepository.IsUserLoggedIn())
            {
                try
                {
                    _context.Histories.Add(history);
                    
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                }
                return history;
            }

            return null;
        }


        //   R e a d
        public IQueryable<History> GetAllHistories()
        {
            if (_userRepository.IsUserLoggedIn())
            {
                return _context.Histories;
            }
            History[] noHistory = new History[0];
            return noHistory.AsQueryable<History>();
        }
        public History GetHistoryById(int historyId)
        {
            // return _context.Histories.Find(historyId);
            return _context.Histories.Include(h => h.Pet).Where(h => h.Id == historyId).FirstOrDefault();
        }

        //   U p d a t e
        public History UpdateHistory(History history)
        {
            History historyToUpdate = _context.Histories.SingleOrDefault(p => p.Id == history.Id);
            if (historyToUpdate != null)
            {
                historyToUpdate.Date = history.Date;
                historyToUpdate.Summary = history.Summary;
                historyToUpdate.Weight = history.Weight;
                _context.SaveChanges();
            }

            return historyToUpdate;
        }

        //   D e l e t e

        public bool DeleteHistory(int id)
        {
            History historyToDelete = _context.Histories.Find(id);
            if (historyToDelete == null)
            {
                return false;
            }

            _context.Histories.Remove(historyToDelete);
            _context.SaveChanges();
            return true;
        }
    }
}
