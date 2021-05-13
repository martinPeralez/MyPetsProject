using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPets.Models
{
    public class EfUserRepository : IUserRepository
    {
        //   F i e l d s   &   P r o p e r t i e s

        private AppDbContext _context;
        private ISession _session;
        //   C o n s t r u c t o r s

        public EfUserRepository(AppDbContext context, IHttpContextAccessor httpContext)// , ISession session)
        {
            _context = context;
            // _session = session;
            // _session = HttpContext.Session;
            // HttpContext
            _session = httpContext.HttpContext.Session;
         }

        //   M e t h o d s

        //   C r e a t e
        public User Create(User u)
        {
            User existingUser = GetUserByEmailAddress(u.Email);
            if (existingUser != null)
            {
                return null;
            }
            try
            {
                _context.Users.Add(u);
                _context.SaveChanges();
                return u;
            }
            catch (Exception e)
            {
            }
            return null;
        }

        //   R e a d

        public IQueryable<User> GetAllUsers()
        {
            return _context.Users;
        }
        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
        public User GetUserByEmailAddress(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }
        public bool Login(User user)
        {
            User existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            
            if (existingUser == null || existingUser.Password != user.Password)
            {
                return false;
            }
            else
            {
                _session.SetInt32("userid", existingUser.Id);
                _session.SetString("email", existingUser.Email);
                return true;
            }
        }
        public void Logout()
        {
            _session.Remove("userid");
            _session.Remove("email");
        }
        public bool IsUserLoggedIn()
        {
            int? userId = _session.GetInt32("userid");
            if (userId == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public int GetLoggedInUserId()
        {
            int? userId = _session.GetInt32("userid");
            if (userId == null)
            {
                return -1;
            }
            else
            {
                return userId.Value;
            }
        }
        public string GetLoggedInEmail()
        {
            return _session.GetString("username");
        }
        //   U p d a t e

        public User Update(User u)
        {
            User userToUpdate = GetUserById(u.Id);
            if (userToUpdate != null)
            {
                userToUpdate.IsAdmin = u.IsAdmin;
                userToUpdate.Password = u.Password;
                userToUpdate.PhoneNumber = u.PhoneNumber;
                userToUpdate.Address = u.Address;
                _context.SaveChanges();
            }
            return userToUpdate;
        }
        public bool ChangePassword(string oldPassword, string newPassword)
        {
            if (!IsUserLoggedIn())
            {
                return false;
            }
            else
            {
                User userToUpdate = GetUserById(GetLoggedInUserId());
                if (userToUpdate != null && userToUpdate.Password == oldPassword)
                {
                    userToUpdate.Password = newPassword;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        //   D e l e t e

        public bool Delete(User u)
        {
            return Delete(u.Id);
        }
        public bool Delete(int id)
        {
            User userToDelete = GetUserById(id);
            if (userToDelete == null)
            {
                return false;
            }
            _context.Remove(userToDelete);
            _context.SaveChanges();
            return true;
        }
    }
}
