using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameTube_RESTful.Models;
using GameTube_RESTful.Data;

namespace GameTube_RESTful.Services
{
    public class UserServices(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        public List<User> GetAll()
        {
            return [.. _context.User];
        }

        public User GetUserById(int id)
        {
            return _context.User.FirstOrDefault(u => u.UserId == id);
        }

        public void AddUser(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
        }

        public bool UserExists(string email)
        {
            return _context.User.Any(u => u.Email == email);
        }

        public User GetUserByEmail(string email)
        {
            return _context.User.SingleOrDefault(u => u.Email == email);
        }
    }
}
