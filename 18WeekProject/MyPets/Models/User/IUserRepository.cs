using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPets.Models
{
    public interface IUserRepository
    {
        //   C r e a t e

        public User Create(User u);

        //   R e a d

        public IQueryable<User> GetAllUsers();
        public User GetUserById(int id);
        public User GetUserByEmailAddress(string email);
        public bool Login(User u);
        public void Logout();
        public bool IsUserLoggedIn();
        public int GetLoggedInUserId();
        public string GetLoggedInEmail();
        public bool ChangePassword(string oldPassword, string newPassword);

        //   U p d a t e

        public User Update(User u);

        //   D e l e t e

        public bool Delete(User u);
        public bool Delete(int id);
        public string GenerateRandomPassword();
    }
}
