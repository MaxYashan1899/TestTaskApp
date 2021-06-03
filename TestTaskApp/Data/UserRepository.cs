using System;
using System.Linq;
using TestTaskApp.Models;

namespace TestTaskApp.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;
        public UserRepository(UserContext context)
        {
            _context = context;
        }
        public User Create(User user, string login)
        {
            if (_context.Users.Any(u => u.Login == login))
            {
                throw new Exception("Login is already used");
            }
            _context.Users.Add(user);
            user.Id = _context.SaveChanges();
            return user;
        }

        public User GetByLogin(string login)
        {
            return _context.Users.FirstOrDefault(u => u.Login == login);
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public bool ExistsByLogin(string login)
        {
            return _context.Users
                  .Any(x => x.Login == login);
        }
    }
}
